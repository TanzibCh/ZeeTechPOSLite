using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLibrary.Models;
using System.Linq;

namespace DataAccessLibrary.DataAccess.Queries
{
    public class SupplierData
    {
        private SQLiteDataAccess _db = new SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        #region Save Supplier

        public void SaveNewSupplier(string supplierName)
        {
            string sql = @"INSERT INTO Supplier
                          (SupplierName)
                          VALUES (@supplierName);";

            _db.SaveData(sql, new { supplierName }, _connectionStringName);
        }

        public void UpdateSupplier(int id, string supplierName)
        {
            string sql = @"UPDATE Supplier
                           SET SupplierName = @supplierName
                           WHERE Id = @id;";

            _db.SaveData(sql, new { id, supplierName }, _connectionStringName);
        }

        public void ChangeSupplierActiveStatus(int id, int activeStatus)
        {
            string sql = @"UPDATE Supplier
                           SET IsActive = @activeStatus
                           WHERE Id = @id;";

            _db.SaveData(sql, new { id, activeStatus }, _connectionStringName);
        }
        #endregion

        #region Get Supplier

        public List<SupplierModel> GetAllSuppliers()
        {
            string sql = @"SELECT Id, SupplierName, IsActive
                          FROM Supplier;";

            return _db.LoadData<SupplierModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public List<SupplierModel> GetAllActiveSuppliers()
        {
            string sql = @"SELECT Id, SupplierName, IsActive
                          FROM Supplier
                          WHERE IsActive = 1";

            return _db.LoadData<SupplierModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public SupplierModel GetSupplierById(int id)
        {
            string sql = @"SELECT Id, SupplierName, IsActive
                          FROM Supplier
                          WHERE Id = @id";

            return _db.LoadData<SupplierModel, dynamic>(sql, new { id }, _connectionStringName).FirstOrDefault();
        }
        #endregion
    }
}
