using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.DataAccess.Queries
{
    // This class saves, loads and updates
    // EmailAddress data in the database
    public class EmailAddressData
    {
        private const string _connectionStringName = "SQLiteDB";
        private readonly SQLiteDataAccess _db = new SQLiteDataAccess();

        #region Save EmailAddress

        public void AddNewEmailAddress(EmailAddressModel emailAddress)
        {
            string sql = @"INSERT INTO EmailAddress
                          (CustomerId, SupplierId, LocationId, EmailAddress)
                          VALUES (@customerId, @supplierId, @locationId, @emailAddress);";


            _db.SaveData(sql, new
            {
               customerId = emailAddress.CustomerId,
               supplierId = emailAddress.SupplierId,
               locationId = emailAddress.LocationId,
               emailAddress = emailAddress.EmailAdderss
            }, _connectionStringName);
        }

        public void UpdateEmailAddress(int id, string emailAddress)
        {
            string sql = @"UPDATE EmailAddress
                          SET EmailAddress = @emailAddress
                          WHERE Id = @id";

            // Save the data
            _db.SaveData(sql, new { id, emailAddress }, _connectionStringName);
        }
        #endregion

        #region Get EmailAddress

        public EmailAddressModel GetEmailById(int id)
        {
            string sql = @"SELECT e.Id, e.CustomerId, e.SupplierId, e.LocationId, e.EmailAddress, c.CustomerName, s.SupplierName, l.LocationName
                          FROM EmailAddress e
                          INNER JOIN Customer c on e.CustomerId = c.Id
                          INNER JOIN Supplier s on e.SupplierId = s.Id
                          INNER JOIN Location l on e.LocationId = l.Id
                          WHERE e.Id = @id;";

            return _db.LoadData<EmailAddressModel, dynamic>(sql, new { id }, _connectionStringName).FirstOrDefault();
        }

        public List<EmailAddressModel> GetAllEmailAddress()
        {
            string sql = @"SELECT e.Id, e.CustomerId, e.SupplierId, e.LocationId, e.EmailAddress, c.CustomerName, s.SupplierName, l.LocationName
                          FROM EmailAddress e
                          INNER JOIN Customer c on e.CustomerId = c.Id
                          INNER JOIN Supplier s on e.SupplierId = s.Id
                          INNER JOIN Location l on e.LocationId = l.Id;";

            return _db.LoadData<EmailAddressModel, dynamic>(sql, new {  }, _connectionStringName);
        }

        public List<EmailAddressModel> GetAllCustomerEmailAddress(int customerId)
        {
            string sql = @"SELECT e.Id, e.CustomerId, e.SupplierId, e.LocationId, e.EmailAddress, c.CustomerName, s.SupplierName, l.LocationName
                          FROM EmailAddress e
                          INNER JOIN Customer c on e.CustomerId = c.Id
                          INNER JOIN Supplier s on e.SupplierId = s.Id
                          INNER JOIN Location l on e.LocationId = l.Id
                          WHERE e.CustomerId = @customerId;";

            return _db.LoadData<EmailAddressModel, dynamic>(sql, new { customerId }, _connectionStringName);
        }

        public List<EmailAddressModel> GetAllSupplierEmailAddress(int supplierId)
        {
            string sql = @"SELECT e.Id, e.CustomerId, e.SupplierId, e.LocationId, e.EmailAddress, c.CustomerName, s.SupplierName, l.LocationName
                          FROM EmailAddress e
                          INNER JOIN Customer c on e.CustomerId = c.Id
                          INNER JOIN Supplier s on e.SupplierId = s.Id
                          INNER JOIN Location l on e.LocationId = l.Id
                          WHERE e.SupplierId = @supplierId;";

            return _db.LoadData<EmailAddressModel, dynamic>(sql, new { supplierId }, _connectionStringName);
        }

        public List<EmailAddressModel> GetAllLocationEmailAddress(int locationId)
        {
            string sql = @"SELECT e.Id, e.CustomerId, e.SupplierId, e.LocationId, e.EmailAddress, c.CustomerName, s.SupplierName, l.LocationName
                          FROM EmailAddress e
                          INNER JOIN Customer c on e.CustomerId = c.Id
                          INNER JOIN Supplier s on e.SupplierId = s.Id
                          INNER JOIN Location l on e.LocationId = l.Id
                          WHERE e.LocationId = @locationId;";

            return _db.LoadData<EmailAddressModel, dynamic>(sql, new { locationId }, _connectionStringName);
        }
        #endregion
    }
}
