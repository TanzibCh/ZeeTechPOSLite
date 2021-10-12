using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.DataAccess.Queries
{
    public class CustomerData
    {
        private Internal.SQLiteDataAccess.SQLiteDataAccess _db = new Internal.SQLiteDataAccess.SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        public void SaveNewCustomer(CustomerModel customer)
        {
            string sql = @"INSERT INTO Customer
                          (FirstName, LastName)
                          VALUES (@firstName, @lastName)";

            _db.SaveData(sql, new 
            {
                firstName = customer.FirstName,
                lastName = customer.LastName
            }, _connectionStringName);
        }

        public void UpdateCustomer(CustomerModel customer)
        {
            string sql = @"UPDATE Customer
                          SET FirstName = @firstName
                          LastName = @lastName
                          WHERE Id = @id";

            _db.SaveData(sql, new
            {
                id = customer.Id,
                firstName = customer.FirstName,
                lastName = customer.LastName
            }, _connectionStringName);
        }

        /// <summary>
        /// Changes the Customer's Active status. If IsActive is true
        /// then change to false and vice versa.
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <param name="activeStatus">If you want to deactivate the set 0 or to avtivate 1</param>
        public void ChangeCustomerActiveStatus(int id, int activeStatus)
        {
            string sql = @"UPDATE Customer
                          SET IsActive = @isActive
                          WHERE Id = @id";

            _db.SaveData(sql, new
            {
                id = id,
                isActive = activeStatus
            }, _connectionStringName);
        }

        public List<CustomerModel> GetAllCustomer()
        {
            string sql = @"SELECT Id, FirstName, LastName
                          FROM Customer
                          WHERE IsActive = 1;";

            return _db.LoadData<CustomerModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public CustomerModel GetCustomerById(int id)
        {
            string sql = @"SELECT Id, FirstName, LastName
                          FROM Customer
                          WHERE IsActive = 1
                          AND Id = @id;";

            return _db.LoadData<CustomerModel, dynamic>(sql, new { id }, _connectionStringName).FirstOrDefault();
        }

        public List<CustomerModel> GetCustomerByFirstName(string firstName)
        {
            string sql = @"SELECT Id, FirstName, LastName
                          FROM Customer
                          WHERE IsActive = 1
                          AND FirstName like ?";

            firstName = "%" + firstName + "%";

            return _db.LoadData<CustomerModel, dynamic>(sql, new { firstName }, _connectionStringName);
        }

        public List<CustomerModel> GetCustomerByLastName(string lastName)
        {
            string sql = @"SELECT Id, FirstName, LastName
                          FROM Customer
                          WHERE IsActive = 1
                          AND LastName like ?";

            lastName = "%" + lastName + "%";

            return _db.LoadData<CustomerModel, dynamic>(sql, new { lastName }, _connectionStringName);
        }
    }
}
