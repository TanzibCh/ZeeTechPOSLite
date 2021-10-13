using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.DataAccess.AddressQueries
{
    public class AddressData
    {
        private Internal.SQLiteDataAccess.SQLiteDataAccess _db = new Internal.SQLiteDataAccess.SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        #region Save Address

        /// <summary>
        /// Creates a new Address for Customer, Location or Supplier
        /// determined by the provided Id from the AddressModel parameter
        /// </summary>
        /// <param name="address">AddressModel</param>
        public void AddNewAddress(AddressModel address)
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
        #endregion

        #region Get Address

        /// <summary>
        /// Get a single Address by the Id propvided
        /// </summary>
        /// <param name="id">Id of the Adderess intended, could be Customer, Supplier or Location address</param>
        /// <returns></returns>
        public AddressModel GetAddressById(int id)
        {
            string sql = @"SELECT Id, CustomerId, SupplierId, LocationId, Address
                          FROM Address
                          WHERE IsActive = 1
                          and Id = @id;";

            return _db.LoadData<AddressModel, dynamic>(sql, new { id }, _connectionStringName).FirstOrDefault();
        }

        /// <summary>
        /// Get all the addresses for the selected customer
        /// </summary>
        /// <param name="customerId">Id of the Customer</param>
        /// <returns></returns>
        public List<AddressModel> GetAddressByCustomerId(int customerId)
        {
            string sql = @"SELECT Id, CustomerId, SupplierId, LocationId, Address
                          FROM Address
                          WHERE IsActive = 1
                          AND CustomerId = @customerId;";

            return _db.LoadData<AddressModel, dynamic>(sql, new { customerId }, _connectionStringName);
        }

        /// <summary>
        /// Get all the addresses for the selected Supplier
        /// </summary>
        /// <param name="supplierId">Id of the Supplier</param>
        /// <returns></returns>
        public List<AddressModel> GetAddressBySupplierId(int supplierId)
        {
            string sql = @"SELECT Id, CustomerId, SupplierId, LocationId, Address
                          FROM Address
                          WHERE IsActive = 1
                          AND SupplierId = @supplierId;";

            return _db.LoadData<AddressModel, dynamic>(sql, new { supplierId }, _connectionStringName);
        }

        /// <summary>
        /// Get all the addresses for the selected Location
        /// </summary>
        /// <param name="locationId">Id of the Location</param>
        /// <returns></returns>
        public List<AddressModel> GetAddressByLocationId(int locationId)
        {
            string sql = @"SELECT Id, CustomerId, SupplierId, LocationId, Address
                          FROM Address
                          WHERE IsActive = 1
                          AND LocationId = @locationId;";

            return _db.LoadData<AddressModel, dynamic>(sql, new { locationId }, _connectionStringName);
        }

        // Get all Customer Addresses 
        public List<AddressModel> GetAllCustomerAddress()
        {
            string sql = @"SELECT Id, CustomerId, SupplierId, LocationId, Address
                          FROM Address
                          WHERE IsActive = 1
                          AND SupplierId = 0
                          AND LocationId = 0;";

            return _db.LoadData<AddressModel, dynamic>(sql, new { }, _connectionStringName);
        }

        // Get all Supplier Addresses 
        public List<AddressModel> GetAllSupplierAddress()
        {
            string sql = @"SELECT Id, CustomerId, SupplierId, LocationId, Address
                          FROM Address
                          WHERE IsActive = 1
                          AND CustomerId = 0
                          AND LocationId = 0;";

            return _db.LoadData<AddressModel, dynamic>(sql, new { }, _connectionStringName);
        }

        // Get all Location Addresses 
        public List<AddressModel> GetAllLocationAddress()
        {
            string sql = @"SELECT Id, CustomerId, SupplierId, LocationId, Address
                          FROM Address
                          WHERE IsActive = 1
                          AND CustomerId = 0
                          AND SupplierId = 0;";

            return _db.LoadData<AddressModel, dynamic>(sql, new { }, _connectionStringName);
        }
        #endregion
    }
}
