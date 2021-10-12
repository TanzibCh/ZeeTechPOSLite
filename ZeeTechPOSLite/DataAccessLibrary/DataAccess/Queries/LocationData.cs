using DataAccessLibrary.DataAccess.Internal.SQLiteDataAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.DataAccess.Queries
{
    public class LocationData
    {
        private Internal.SQLiteDataAccess.SQLiteDataAccess _db = new Internal.SQLiteDataAccess.SQLiteDataAccess();
        private const string _connectionStringName = "SQLiteDB";

        #region Save Location

        public void AddNewLocation(LocationModel location)
        {
            // Query to save Address data
            string sql = @"INSERT INTO Location
                          (LocationName)
                          VALUES (@locationName);";


            // Insert Address data into the database
            _db.SaveData(sql, new
            {
                locationName = location.LocationName
            }, _connectionStringName);
        }

        public void UpdateLocation(int id, string locationName)
        {
            string sql = @"UPDATE Location
                           SET LocationName = @locationName
                           WHERE Id = @id;";

            _db.SaveData(sql, new { id, locationName }, _connectionStringName);
        }
        #endregion

        #region Get Location

        public List<LocationModel> GetAllLocations()
        {
            string sql = @"SELECT Id, LocationName, IsActive
                          FROM Location
                          WHERE IsActive = 1;";

            return _db.LoadData<LocationModel, dynamic>(sql, new { }, _connectionStringName);
        }

        public LocationModel GetLocationsById(int id)
        {
            string sql = @"SELECT Id, LocationName, IsActive
                          FROM Location
                          WHERE IsActive = 1
                          AND Id = @id;";

            return _db.LoadData<LocationModel, dynamic>(sql, new { id }, _connectionStringName).
                FirstOrDefault();
        }
        #endregion
    }
}
