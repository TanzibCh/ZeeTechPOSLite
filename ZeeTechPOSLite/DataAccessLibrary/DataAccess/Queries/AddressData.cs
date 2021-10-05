using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess.AddressQueries
{
    public class AddressData
    {
        private SQLiteDataAccess _db = new SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        /// <summary>
        /// Creates a new Address for Customer, Location or Supplier
        /// determined by the provided Id from the AddressModel parameter
        /// </summary>
        /// <param name="address">AddressModel</param>
        public void CreteNewAddress(AddressModel address)
        {
            // Query to save Address data
            string sql = @"INSERT INTO Address
                          (CustomerId, SupplierId, LocationId, Address)
                          VALUES (@customerId, @supplierId, @locationId, @address);";


            // Insert Address data into the database
            _db.SaveData(sql, new
            {
                customerId = address.CustomerId,
                supplierId = address.SupplierId,
                locationId = address.LocationId,
                address = address.Address
            }, _connectionStringName);

        }

        public void UpdateAddress(int addressId, string address)
        {
            string sql = @"UPDATE Address
                           SET Address = @address
                           WHERE Id = @id;";

            _db.SaveData(sql, new 
            {
                id = addressId,
                address = address
            }, _connectionStringName);
        }

        /// <summary>
        /// Changes the address active staus. If current status 
        /// is active then change to notActive and vice versa.
        /// </summary>
        /// <param name="id">Id of the Address</param>
        /// <param name="activeStatus">Enter 0 if you want it to be false or 1 for true</param>
        public void ChangeAddressActiveStatus(int id, int activeStatus)
        {
            string sql = @"UPDATE Sale
                           SET IsActive = @isActive
                           WHERE Id = @id;";

            _db.SaveData(sql, new
            {
                id = id,
                isActive = activeStatus
            }, _connectionStringName);
        }
    }
}
