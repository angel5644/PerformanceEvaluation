using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.DBContext
{
    public class PESDBContext
    {
        public string DataSource
        {
            get { return "localhost:1521/xe"; }
        }
        public string UserId
        {
            get { return "pe"; }
        }
        public string Password
        {
            get { return "colima4"; }
        }
        public string ConnectionString
        {
            get
            {
                return "data source=" + this.DataSource + ";user id=" + this.UserId + ";password=" + this.Password;
                //"data source=localhost;user id=system;password=4colima";
            }
        }

        public OracleConnection GetDBConnection()
        {
            try
            {
                return new OracleConnection(this.ConnectionString);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}