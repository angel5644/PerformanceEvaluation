using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using PES.DBContext;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class SubtitleService
    {
        private PESDBContext dbContext = new PESDBContext();

        public bool InsertSubtitles(Subtitle subtitle)
        {
            bool status = false;
            using(OracleConnection db = dbContext.GetDBConnection())
            {
                string insertQuery = @"INSERT INTO SUBTITLE (SUBTITLE, 
                                                            ID_TITLE) 
                                        VALUES (:Name, 
                                                :TitleId)";

                using (OracleCommand Command = new OracleCommand(insertQuery, db))
                {
                    Command.Parameters.Add(new OracleParameter("Name", subtitle.Name));
                    Command.Parameters.Add(new OracleParameter("TitleId", subtitle.TitleId));

                    try
                    {
                        Command.Connection.Open();
                        Command.ExecuteNonQuery();
                        Command.Connection.Close();
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