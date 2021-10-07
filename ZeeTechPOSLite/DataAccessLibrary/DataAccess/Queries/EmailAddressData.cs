using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
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
            string sql = @"SELECT s.ProductId, s.LocationId, s.Quantity, p.ProductName, l.LocationName
                          FROM EmailAddress e
                          INNER JOIN Customer c on e.CustomerId = c.Id
                          INNER JOIN Supplier s on e.SupplierId = s.Id
                          INNER JOIN Location l on s.LocationId = l.Id;";

            return _db.LoadData<CustomerModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public List<EmailAddressModel> GetAllEmailAddress()
        {

        }

        public List<EmailAddressModel> GetAllCustomerEmailAddress()
        {

        }

        public List<EmailAddressModel> GetAllSupplierEmailAddress()
        {

        }

        public List<EmailAddressModel> GetAllLocationEmailAddress()
        {

        }
        #endregion
    }
}
