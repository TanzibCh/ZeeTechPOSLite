
using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLibrary.DataAccess.SalesQueries
{
    // This class saves, loads and updates
    // Sale data in the database
    public class SalesData
    {
        private SQLiteDataAccess _db = new SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        #region Save Sale

        private int GetLatestSaleId()
        {
            string sql = @"SELECT Id
                           FROM Sale
                           ORDER BY Id DESC
                           LIMIT 1;";

            SaleModel sale = _db.LoadData<SaleModel, dynamic>(sql, new { }, _connectionStringName).FirstOrDefault();

            return sale.Id;
        }

        // Queries for the last Invoice in the database and adds 1 to it to get the new Invoice number
        private int GetNewInvoiceNo(int cashOnly)
        {
            string sql = @"SELECT InvoiceNo
                           FROM sale
                           WHERE CashOnly = @cashOnly
                           ORDER BY InvoiceNo DESC
                           LIMIT 1;";

            SaleModel sale = _db.LoadData<SaleModel, dynamic>(
                sql, new { cashOnly }, _connectionStringName).FirstOrDefault();

            int output;
            if (sale == null)
            {
                output = 1;
            }
            else
            {
                output = sale.InvoiceNo + 1;
            }

            return output;
        }

        private void SaveSaleDetails(SaleModel saleInfo)
        {
            // Query to save Sale data
            string sql = @"INSERT INTO Sale
                          (InvoiceNo, SaleDate, SaleTime, Card, Cash, Credit, SaleTotal, Tax, TotalCost, Profit, CashOnly)
                          values (@invoiceNo, @saleDate, @saleTime, @card, @cash, @credit, @saleTotal, @tax, @totalCost, @profit, @cashOnly);";

            // Insert sale data into the database
            _db.SaveData(sql, new
            {
                invoiceNo = saleInfo.InvoiceNo,
                saleDate = saleInfo.SaleDate,
                saleTime = saleInfo.SaleTime,
                card = saleInfo.Card,
                cash = saleInfo.Cash,
                credit = saleInfo.Credit,
                saleTotal = saleInfo.SaleTotal,
                tax = saleInfo.Tax,
                totalCost = saleInfo.TotalCost,
                profit = saleInfo.Profit,
                cashOnly = saleInfo.CashOnly
            }, _connectionStringName);
        }

        private void SaveSaleProducts(List<SaleProductModel> saleProducts, int saleId)
        {
            string sql = @"INSERT INTO SaleProduct
                          (SaleId, ProductId, ProductName, ProductDescription, SalePrice, ProductCost, QuantitySold, Total, Department)
                          values (@saleId, @productId, @Name, @description, @price, @cost, @quantitySold, @total, @department);";

            foreach (var item in saleProducts)
            {
                _db.SaveData(sql, new
                {
                    saleId = saleId,
                    productId = item.ProductId,
                    Name = item.ProductName,
                    description = item.ProductDescription,
                    price = item.SalePrice,
                    cost = item.ProductCost,
                    quantitySold = item.QuantitySold,
                    total = item.SalePrice * item.QuantitySold,
                    department = item.Department
                }, _connectionStringName);
            }
        }

        public void SaveSale(SaleModel saleInfo, List<SaleProductModel> saleProducts)
        {
            // TODO: Make it SOLID in the future
            // Get new Invoice number
            saleInfo.InvoiceNo = GetNewInvoiceNo(saleInfo.CashOnly);

            // Save the sale with the new Invoice Number
            SaveSaleDetails(saleInfo);

            // Get the latest Saleid
            int saleId = GetLatestSaleId();

            // save the list of Sale Products with the newly created SaleId
            SaveSaleProducts(saleProducts, saleId);
        }

        #endregion Save Sale

        #region Update Sale

        // Change the IsActive status of a sale If Active change to not active, if not active chage
        // to active
        public void ChangeSaleActiveStatus(int id, bool activeStatus)
        {
            int isActive = 0;
            if (activeStatus == true)
            {
                isActive = 0;
            }
            else
            {
                isActive = 1;
            }

            string sql = @"UPDATE Sale
                           SET IsActive = @isActive
                           WHERE Id = @id;";

            _db.SaveData(sql, new
            {
                Id = id,
                isActive = isActive
            }, _connectionStringName);
        }

        // Updates sales details and sale products
        public void UpdateSale(SaleModel sale, List<SaleProductModel> saleProducts)
        {
            UpdateSaleDetails(sale);

            UpdateSaleProducts(saleProducts, sale.Id);
        }

        // Updates sales Details
        private void UpdateSaleDetails(SaleModel sale)
        {
            string sql = @"UPDATE Sale
                           SET Card = @card,
                               Cash = @cash,
                               Credit = @credit,
                               SaleTotal = @saleTotal,
                               Tax = @tax,
                               TotalCost = @totalCost,
                               Profit = @profit
                           WHERE Id = @id;";

            _db.SaveData(sql, new
            {
                card = sale.Card,
                cash = sale.Cash,
                credit = sale.Credit,
                saleTotal = sale.SaleTotal,
                tax = sale.Tax,
                totalCost = sale.TotalCost,
                profit = sale.Profit,
                id = sale.Id
            }, _connectionStringName);
        }

        // Updates sales products
        private void UpdateSaleProducts(List<SaleProductModel> saleProducts, int saleId)
        {
            string sql = @"UPDATE SaleProduct
                           SET ProductName = @productName,
                               ProductDescription = @productDescription,
                               Department = @department,
                               SalePrice = @salePrice,
                               ProductCost = @productCost,
                               QuantitySold = @quantitySold,
                               total = @total
                           WHERE Id = @id;";

            foreach (SaleProductModel product in saleProducts)
            {
                _db.SaveData(sql, new
                {
                    id = product.Id,
                    productName = product.ProductName,
                    productDescription = product.ProductDescription,
                    department = product.Department,
                    salePrice = product.SalePrice,
                    productCost = product.ProductCost,
                    quantitySold = product.QuantitySold,
                    total = product.Total
                }, _connectionStringName);
            }
        }

        #endregion Update Sale

        #region Get Sale
        public List<SaleModel> GetCashOnlySalesByDate(string selectedDate)
        {
            string sql = @"SELECT Id, InvoiceNo, SaleDate, SaleTime, Card, Cash, Credit, SaleTotal, Tax, TotalCost, Profit, CashOnly
                          FROM Sale
                          WHERE SaleDate = @selectedDate
                          AND CashOnly = 1
                          AND IsActive = 1;";

            List<SaleModel> sales = _db.LoadData<SaleModel, dynamic>(sql, new { selectedDate }, _connectionStringName);

            return sales;
        }

        public List<SaleProductModel> GetSalesByDepartmentAndDate(string selectedDate, string departmentName)
        {
            string sql = @"SELECT sp.SaleId, s.InvoiceNo, s.SaleDate, sp.ProductId, sp.ProductName,
                           sp.ProductDescription, sp.SalePrice, sp.ProductCost, sp.QuantitySold, sp.Department
                           FROM Sale s
                           INNER JOIN SaleProduct sp on sp.SaleId = s.Id
                           WHERE s.SaleDate = @selectedDate
                           AND sp.Department = @departmentName
                           AND s.IsActive = 1;";

            List<SaleProductModel> sales = _db.LoadData<SaleProductModel, dynamic>(sql, new { selectedDate, departmentName }, _connectionStringName);

            return sales;
        }

        public List<SaleModel> GetAllSalesByDate(string selectedDate)
        {
            string sql = @"SELECT Id, InvoiceNo, SaleDate, SaleTime, Card, Cash, Credit, SaleTotal, Tax, TotalCost, Profit, CashOnly, IsActive
                          FROM Sale
                          WHERE SaleDate = @selectedDate
                          AND IsActive = 1;";

            List<SaleModel> sales = _db.LoadData<SaleModel, dynamic>(sql, new { selectedDate }, _connectionStringName);

            return sales;
        }

        public SaleModel GetSaleById(int id)
        {
            string sql = @"SELECT Id, InvoiceNo, SaleDate, SaleTime, Card, Cash, Credit, SaleTotal, Tax, TotalCost, Profit, CashOnly
                          FROM Sale
                          WHERE Id = @id";

            SaleModel sale = _db.LoadData<SaleModel, dynamic>(sql, new { id }, _connectionStringName).FirstOrDefault();

            return sale;
        } 
        #endregion
    }
}