using Oracle.ManagedDataAccess.Client;
using PES.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.Services
{
    public class LocationService
    {
        private PESDBContext dbContext;

        public LocationService()
        {
            dbContext = new PESDBContext();
        }

        //Get all locations
        public List<Location> GetAll()
        {
            List<Location> locations = new List<Location>();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = "SELECT ID_LOCATION, " +
                                           "NAME " +
                                           "FROM LOCATION";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {

                        // Store data in period object 
                        Location location = new Location();
                        location.LocationId = Convert.ToInt32(Read["ID_LOCATION"]);
                        location.Name = Convert.ToString(Read["NAME"]);

                        locations.Add(location);
                    }
                    db.Close();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return locations;
        }

        // Get a location by id
        public Location GetPeriodById(int id)
        {
            Location location = new Location();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT ID_LOCATION," +
                                    "NAME " +
                                    "FROM LOCATION WHERE ID_LOCATION = '" + id + "'";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {
                        // Store data in a period object 
                        location = new Location();
                        location.LocationId = Convert.ToInt32(Read["ID_LOCATION"]);
                        location.Name = Convert.ToString(Read["NAME"]);
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return location;
        }
    }
}