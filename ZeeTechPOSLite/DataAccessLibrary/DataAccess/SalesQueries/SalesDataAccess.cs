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

        public void SaveSale(SaleDBModel saleInfo, List<SaleProductModel> saleProducts)
        {

            // TODO: Make it SOLID in the future

            // Get new Invoice number
            saleInfo.InvoiceNo = GetNewInvoiceNo(saleInfo.CashOnly);

            // Set CashIn as int to save in database
            //int cashInInvoice = 0;
            //if (saleInfo.CashOnly == true)
            //{
            //    cashInInvoice = 1;
            //}
            //else
            //{
            //    cashInInvoice = 0;
            //}

            // Create Sale and save it in database
            string sql = @"INSERT INTO Sale
                          (InvoiceNo, SaleDate, SaleTime, Card, Cash, Credit, Total, Tax, Profit, CashIn)
                          values (@invoiceNo, @saleDate, @saleTime, @card, @cash, @credit, @total, @tax, @profit, @cashIn);";
 
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
                    cashIn = saleInfo.CashOnly
            }, "Default");

            int saleId = GetLatestSaleId();

            SaveSaleDetails(saleProducts, saleId);
        }

        private int GetLatestSaleId()
        {
            string sql = @"SELECT max(SaleId) from
                          Sale;";

            return _db.LoadData<dynamic, dynamic>(sql, new { }, "Default").FirstOrDefault();
        }

        // Queries for the last Invoice in the database and adds 1 to it to get the new Invoice number
        private int GetNewInvoiceNo(bool cashOnly)
        {
            string sqlCashOnlyTrue = @"SELECT max(InvoiceNo) from
                                      sale
                                      WHERE CashIn = 1;";

            string sqlCashOnlyFalse = @"SELECT max(InvoiceNo) from
                                       sale
                                       WHERE CashIn = 0;";

            int lastInvoiceNo = 0;

            if (cashOnly == true)
            {
                lastInvoiceNo = _db.LoadData<dynamic, dynamic>(sqlCashOnlyTrue, new { }, "Default").FirstOrDefault();
            }
            else
            {
                lastInvoiceNo = _db.LoadData<dynamic, dynamic>(sqlCashOnlyFalse, new { }, "Default").FirstOrDefault();
            }

            int output = lastInvoiceNo + 1;
            return output;
        }

        private void SaveSaleDetails(List<SaleProductModel> saleProducts, int saleId)
        {
            string sql = @"INSERT INTO SaleProduct
                          (SaleId, ProductId, ProductName, ProductDescription, SalePrice, ProductCost, Department)
                          values (@saleId, @productId, @Name, @description, @price, @cost, @department);";


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
                        department = item.Department 
                    }, "Default");
            }
        }
    }
}
