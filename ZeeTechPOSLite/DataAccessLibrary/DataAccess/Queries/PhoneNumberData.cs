using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.DataAccess.Queries
{
    public class PhoneNumberData
    {
        private SQLiteDataAccess _db = new SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        #region Save Phone Number

        public void AddNewPhoneNumber(PhoneNumberModel phoneNumber)
        {
            // Query to save Address data
            string sql = @"INSERT INTO PhoneNumber
                          (PhoneNumber)
                          VALUES (@number);";


            // Insert Address data into the database
            _db.SaveData(sql, new
            {
                number = phoneNumber.Number
            }, _connectionStringName);
        }

        public void UpdatePhoneNumber(int id, string number)
        {
            string sql = @"UPDATE PhoneNumber
                           SET Number = @number
                           WHERE Id = @id;";

            _db.SaveData(sql, new { id, number }, _connectionStringName);
        }
        #endregion

        #region Get PhoneNumber

        public List<PhoneNumberModel> GetAllPhoneNumbers()
        {
            string sql = @"SELECT p.Id, p.CustomerId, p.SupplierId, 
                          p.LocationId, p.Number, c.CustomerName, s.SupplierName, l.LocationName
                          FROM PhoneNumber p
                          INNER JOIN Customer c on p.CustomerId = c.Id
                          INNER JOIN Supplier s on p.SupplierId = s.Id
                          INNER JOIN Location l on p.LocationId = l.Id;";

            return _db.LoadData<PhoneNumberModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public PhoneNumberModel GetEmailById(int id)
        {
            string sql = @"SELECT p.Id, p.CustomerId, p.SupplierId, 
                          p.LocationId, p.Number, c.CustomerName, s.SupplierName, l.LocationName
                          FROM PhoneNumber p
                          INNER JOIN Customer c on p.CustomerId = c.Id
                          INNER JOIN Supplier s on p.SupplierId = s.Id
                          INNER JOIN Location l on p.LocationId = l.Id
                          WHERE p.Id = @id;";

            return _db.LoadData<PhoneNumberModel, dynamic>(sql, new { id }, _connectionStringName).FirstOrDefault();
        }

        public List<PhoneNumberModel> GetPhoneNumberByCustomerId(int customerId)
        {
            string sql = @"SELECT p.Id, p.CustomerId, p.SupplierId, 
                          p.LocationId, p.Number, c.CustomerName, s.SupplierName, l.LocationName
                          FROM PhoneNumber p
                          INNER JOIN Customer c on p.CustomerId = c.Id
                          INNER JOIN Supplier s on p.SupplierId = s.Id
                          INNER JOIN Location l on p.LocationId = l.Id
                          WHERE p.CustomerId = @customerId;";

            return _db.LoadData<PhoneNumberModel, dynamic>(sql, new { customerId }, _connectionStringName);
        }

        public List<PhoneNumberModel> GetPhoneNumberBySupplierId(int supplierId)
        {
            string sql = @"SELECT p.Id, p.CustomerId, p.SupplierId, 
                          p.LocationId, p.Number, c.CustomerName, s.SupplierName, l.LocationName
                          FROM PhoneNumber p
                          INNER JOIN Customer c on p.CustomerId = c.Id
                          INNER JOIN Supplier s on p.SupplierId = s.Id
                          INNER JOIN Location l on p.LocationId = l.Id
                          WHERE p.SupplierId = @supplierId;";

            return _db.LoadData<PhoneNumberModel, dynamic>(sql, new { supplierId }, _connectionStringName);
        }

        public List<PhoneNumberModel> GetPhoneNumberByLocationId(int locationId)
        {
            string sql = @"SELECT p.Id, p.CustomerId, p.SupplierId, 
                          p.LocationId, p.Number, c.CustomerName, s.SupplierName, l.LocationName
                          FROM PhoneNumber p
                          INNER JOIN Customer c on p.CustomerId = c.Id
                          INNER JOIN Supplier s on p.SupplierId = s.Id
                          INNER JOIN Location l on p.LocationId = l.Id
                          WHERE p.LocationId = @locationId;";

            return _db.LoadData<PhoneNumberModel, dynamic>(sql, new { locationId }, _connectionStringName);
        }
        #endregion
    }
}
