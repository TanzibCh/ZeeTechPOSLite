using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.DataAccess.SalesQueries
{
    public class SalesDataAccess
    {
        SQLiteDataAccess _db = new SQLiteDataAccess();
        private const string connectionStringName = "SQLiteDB";

        public void SaveSale(SaleDBModel saleInfo, List<SaleProductDBModel> saleProducts)
        {
            // TODO: Make it SOLID in the future
            // Get new Invoice number
            saleInfo.InvoiceNo = GetNewInvoiceNo(saleInfo.CashOnly);

            // Save the sale with the new Invoice Number
            SaveSaleDetails(saleInfo);

            // Get the latest created Saleid
            int saleId = GetLatestSaleId();

            // save the list of Sale Products with the newly created SaleId
            SaveSaleProducts(saleProducts, saleId);
        }

        private int GetLatestSaleId()
        {
            string sql = @"SELECT Id 
                           FROM Sale
                           ORDER BY Id DESC
                           LIMIT 1;";

            SaleDBModel sale = _db.LoadData<SaleDBModel, dynamic>(sql, new { }, connectionStringName).FirstOrDefault();

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

            SaleDBModel sale = _db.LoadData<SaleDBModel, dynamic>(sql, new { cashOnly }, connectionStringName).FirstOrDefault();

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

        private void SaveSaleDetails(SaleDBModel saleInfo)
        {
            // Create Sale and save it in database
            string sql = @"INSERT INTO Sale
                          (InvoiceNo, SaleDate, SaleTime, Card, Cash, Credit, Total, Tax, Profit, CashOnly)
                          values (@invoiceNo, @saleDate, @saleTime, @card, @cash, @credit, @total, @tax, @profit, @cashOnly);";

            // Insert sale data into the database
            _db.SaveData(sql, new
            {
                invoiceNo = saleInfo.InvoiceNo,
                saleDate = saleInfo.SaleDate,
                saleTime = saleInfo.SaleTime,
                card = saleInfo.Card,
                cash = saleInfo.Cash,
                credit = saleInfo.Credit,
                total = saleInfo.Total,
                tax = saleInfo.Tax,
                profit = saleInfo.Profit,
                cashOnly = saleInfo.CashOnly
            }, connectionStringName);
        }

        private void SaveSaleProducts(List<SaleProductDBModel> saleProducts, int saleId)
        {
            string sql = @"INSERT INTO SaleProduct
                          (SaleId, ProductId, ProductName, ProductDescription, SalePrice, ProductCost, QuantitySold, Department)
                          values (@saleId, @productId, @Name, @description, @price, @cost, @quantitySold, @department);";

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
                        department = item.Department
                    }, connectionStringName);
            }
        }
    }
}
