using DataAccessLibrary.Models;
using System.Collections.Generic;

namespace DataAccessLibrary.DataAccess.SalesQueries
{
    public interface ISalesDataAccess
    {
        void SaveSale(SaleModel saleInfo, List<SaleProductModel> saleProducts);
    }
}