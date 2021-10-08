using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.DataAccess.Queries
{
    public class DepartmentData
    {
        private SQLiteDataAccess _db = new SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        #region Save Department

        public void AddNewDepartment(string departmentName)
        {
            string sql = @"INSERT INTO Department
                          (DepartmentName)
                          VALUES (@departmentName);";

            // Insert Department in the database
            _db.SaveData(sql, new { departmentName }, _connectionStringName);
        }
        #endregion

        #region Get

        public List<DepartmentModel> GetAllDepartments()
        {
            string sql = @"SELECT Id, DepartmentName
                          FROM Department;";

            return _db.LoadData<DepartmentModel, dynamic>(sql, new { }, _connectionStringName);
        }
        #endregion
    }
}
