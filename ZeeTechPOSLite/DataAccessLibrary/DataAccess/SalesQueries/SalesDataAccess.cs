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
        SQLiteDataAccess db = new SQLiteDataAccess();

        
        public void SaveSale(SaleDBModel saleInfo)
        {

            // TODO: Make it SOLID in the future

            // Get new Invoice number
            int newInvoiceNo = GetNewInvoiceNo(saleInfo.CashIn);

            saleInfo.SaleDate = DateTime.UtcNow.Date.ToString();
            saleInfo.SaleTime = DateTime.UtcNow.TimeOfDay.ToString();

            // Set CashIn as int to save in database
            //int cashInInvoice = 0;
            //if (saleInfo.CashIn == true)
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
            db.SaveData(sql, saleInfo, "Default");
        }

        // Queries for the last Invoice in the database and adds 1 to it to get the new Invoice number
        private int GetNewInvoiceNo(bool cashIn)
        {
            string sqlIn = @"SELECT max(InvoiceNo) from
                          sale
                          WHERE CashIn = 1;";

            string sqlOut = @"SELECT max(InvoiceNo) from
                          sale
                          WHERE CashIn = 0;";

            int lastInvoiceNo = 0;

            if (cashIn == true)
            {
                lastInvoiceNo = db.LoadData<dynamic, dynamic>(sqlIn, new { }, "Default").FirstOrDefault();
            }
            else
            {
                lastInvoiceNo = db.LoadData<dynamic, dynamic>(sqlOut, new { }, "Default").FirstOrDefault();
            }

            int output = lastInvoiceNo + 1;
            return output;
        }

        private void SaveSaleDetails(List<SaleDetailsModel> saleDetails, int saleId)
        {
            string sql = @"INSERT INTO SaleProduct
                          (SaleId, ProductId, ProductName, ProductDescription, SalePrice, ProductCost, Department)
                          values (@saleId, @productId, @Name, @description, @price, @cost, @department);";


            foreach (var item in saleDetails)
            {
                db.SaveData(sql, new { saleId = saleId, productId = saleDetails. , @Name, @description, @price, @cost, @department }, "Default");
            }

        }
    }
}
