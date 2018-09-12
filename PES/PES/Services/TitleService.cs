using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using System.IO;

namespace PES.Services
{
    public class TitleService
    {
        private PESDBContext dbContext = new PESDBContext();


        public bool InsertTitle(Title title)
        {
            bool status = false;

            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string insertTitle = @"INSERT INTO TITLE (TITLE)
                                        VALUES (:Name)";
                using (OracleCommand Command = new OracleCommand(insertTitle, db))
                {
                    Command.Parameters.Add(new OracleParameter("Name", title.Name));

                    try
                    {
                        Command.Connection.Open();
                        Command.ExecuteNonQuery();
                        Command.Connection.Clone();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }
                    status = true;
                }
            }
            return status;
        }

    }
}