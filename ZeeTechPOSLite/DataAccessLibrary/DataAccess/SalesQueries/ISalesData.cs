using DataAccessLibrary.Models;
using System.Collections.Generic;

namespace DataAccessLibrary.DataAccess.SalesQueries
{
    public interface ISalesData
    {
        void SaveSale(SaleModel saleInfo, List<SaleProductModel> saleProducts);
    }
}