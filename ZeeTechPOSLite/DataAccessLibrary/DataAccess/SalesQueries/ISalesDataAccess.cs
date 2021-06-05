using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;

namespace DataAccessLibrary.DataAccess.SalesQueries
{
    public interface ISalesDataAccess
    {
        List<SaleModel> GetAllSalesByDate(string selectedDate);
        void SaveSale(SaleModel saleInfo, List<SaleProductModel> saleProducts);
    }
}