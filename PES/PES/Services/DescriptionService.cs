using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class DescriptionService
    {
        private PESDBContext dbContext = new PESDBContext();


        public bool InsertDescription(Description description)
        {
            bool status = false;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string InsertDescription = @"INSERT INTO DESCRIPTION (DESCRIPTION, 
                                                                ID_SUBTITLE)
                                        VALUES (:DescriptionText, 
                                                    :SubtitleId)";
                using (OracleCommand Command = new OracleCommand(InsertDescription, db))
                {
                    Command.Parameters.Add(new OracleParameter("DescriptionText", description.DescriptionText));
                    Command.Parameters.Add(new OracleParameter("SubtitleId", description.SubtitleId));

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
                    status = true;
                }
            }
            return status;
        }

        public Description GetDescriptionByText(string DescriptionText)
        {
            Description description = new Description();

            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string insertQuery = @"SELECT ID_DESCRIPTION,
                                        DESCRIPTION,
                                        ID_SUBTITLE
                                    FROM DESCRIPTION
                                    WHERE DESCRIPTION LIKE :descriptiontext";
                using (OracleCommand command = new OracleCommand(insertQuery, db))
                {
                    command.Parameters.Add(new OracleParameter("descriptiontext", DescriptionText));

                    try
                    {
                        command.Connection.Open();
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            description = new Description();
                            description.DescriptionId = Convert.ToInt32(reader["ID_DESCRIPTION"]);
                            description.DescriptionText = Convert.ToString(reader["DESCRIPTION"]);
                            description.SubtitleId = Convert.ToInt32(reader["ID_SUBTITLE"]);
                        }
                        command.Connection.Close();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }
                }
            }
            return description;
        }
    }
}