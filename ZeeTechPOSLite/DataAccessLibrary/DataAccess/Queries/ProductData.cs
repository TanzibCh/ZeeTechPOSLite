using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess.Queries
{
    public class ProductData
    {
        private SQLiteDataAccess _db = new SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        #region Save Product

        public void AddNewProduct(ProductModel product)
        {
            // Query to save Address data
            string sql = @"INSERT INTO Product
                          (ProductName, ProductDescription, Barcode, AverageCost
                          Price, Department)
                          VALUES (@productName, @productDescription, @barcode, @averageCost,
                          @price, @department);";


            // Insert Address data into the database
            _db.SaveData(sql, new
            {
                productName = product.ProductName,
                productDescription = product.ProductDescription,
                barcode = product.Barcode,
                averageCost = product.AverageCost,
                price = product.Price,
                department = product.Department
            }, _connectionStringName);
        }

        public void UpdateProduct(ProductModel product)
        {
            string sql = @"UPDATE Product
                           SET ProductName = @productName,
                           ProductDescription = @productDescription
                           Barcode = @barcode
                           AverageCost = @averageCost
                           Price = @price
                           Department = @department
                           IsActive = @isActive
                           WHERE Id = @id;";

            _db.SaveData(sql, new 
            {
                productName = product.ProductName,
                productDescription = product.ProductDescription,
                barcode = product.Barcode,
                averageCost = product.AverageCost,
                price = product.Price,
                department = product.Department,
                isActive = product.IsActive,
                id = product.Id
            }, _connectionStringName);
        }
        #endregion

        #region Get Product


        public List<ProductModel> GetAllActiveProducts()
        {
            string sql = @"SELECT Id, ProductName, ProductDescription, Barcode,
                          AverageCost, Price, Department, IsActive
                          FROM Product
                          WHERE IsActive = 1";

            return _db.LoadData<ProductModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public List<ProductModel> GetAllInActiveProducts()
        {
            string sql = @"SELECT Id, ProductName, ProductDescription, Barcode,
                          AverageCost, Price, Department, IsActive
                          FROM Product
                          WHERE IsActive = 0";

            return _db.LoadData<ProductModel, dynamic>(sql, new { }, _connectionStringName);
        }

        /// <summary>
        /// Method for the Sales page product search. It shows the top 10 selling products.
        /// </summary>
        /// <returns></returns>
        public List<ProductModel> GetTopTenSellingProducts()
        {
            string sql = @"SELECT  p.Id, p.ProductName, p.ProductDescription, p.Barcode,
                          p.AverageCost, p.Price, p.Department, p.IsActive, sum(sp.QuantitySold)
                          FROM Product p
                          INNER JOIN SaleProduct sp on p.Id = sp.ProductId
                          GROUP BY p.Id
                          ORDER By
                          sum(sp.QuantitySold) DESC
                          LIMIT 10;";

            return _db.LoadData<ProductModel, dynamic>(sql, new { }, _connectionStringName);
        }
        #endregion
    }
}
