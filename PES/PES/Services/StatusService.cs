using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class StatusService
    {
        private PESDBContext dbContext = new PESDBContext();

        public Status GetStatusByDescription(string description) 
        {
            Status status = null;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                db.Open();

                string query = "SELECT ID_STATUS," +
                                      "STATUS " +
                               "FROM STATUS WHERE LOWER(STATUS) = '" + description.ToLower() + "'";

                OracleCommand Comand = new OracleCommand(query, db);
                OracleDataReader Read = Comand.ExecuteReader();
                while (Read.Read())
                {

                    // Store data in employee object 
                    status = new Status();
                    status.StatusId = Convert.ToInt32(Read["ID_STATUS"]);
                    status.Description = Convert.ToString(Read["STATUS"]);
                }

                db.Close();
            }

            return status;
        } 
    }
}