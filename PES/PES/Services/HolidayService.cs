using Oracle.ManagedDataAccess.Client;
using PES.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.Services
{
    public class HolidayService
    {
        private PESDBContext dbContext = new PESDBContext();
        public HolidayService()
        {
            dbContext = new PESDBContext();
        }

        public bool EditHoliday(Holiday holiday)
        {
            bool status = false;

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    //working query
                    //UPDATE pe.HOLIDAYS SET HOLIDAYS.HOLIDAY_DAY = '04/12/17', HOLIDAYS.DESCRIPTION = 'Holiday_description' WHERE ID_HOLIDAY = 43;
                    //string query = @"UPDATE pe.HOLIDAYS SET HOLIDAYS.HOLIDAY_DAY = ':Holiday_day', HOLIDAYS.DESCRIPTION = ':Holiday_description' WHERE ID_HOLIDAY = :Holiday_id";
                    string query = @"update pe.holidays set holidays.holiday_day = " + "'" + holiday.InsertDay + "'" + ", holidays.description = " + "'" + holiday.Description + "'" + " where id_holiday = " + holiday.HolidayId;
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("Holiday_id", holiday.HolidayId));
                        command.Parameters.Add(new OracleParameter("Holiday_day", holiday.InsertDay));
                        command.Parameters.Add(new OracleParameter("Holiday_description", holiday.Description));

                        try
                        {
                            command.Connection.Open();
                            command.ExecuteNonQuery();
                            command.Connection.Close();
                        }
                        catch (OracleException ex)
                        {
                            Console.WriteLine(ex.ToString());
                            throw;
                        }
                        status = true;
                    }
                }
            }
            catch (Exception xe)
            {
                throw;
            }
            
            return status;
        }

        public bool DeleteHoliday(Holiday holiday)
        {
            bool status = false;
            int holidayId = holiday.HolidayId;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    string query = @"delete
                                 from 
                                    PE.holidays 
                                 where 
                                    ID_HOLIDAY = :Holiday__id;";
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("Holiday_id", holiday.HolidayId));

                        try
                        {
                            command.Connection.Open();
                            command.ExecuteNonQuery();
                            command.Connection.Close();
                        }
                        catch (OracleException ex)
                        {
                            Console.WriteLine(ex.ToString());
                            throw;
                        }
                        status = true;
                    }
                }
            }

            catch (Exception xe)
            {
                throw;
            }
            return status;
        }

        public bool DeleteHoliday(int holidayId)
        {
            bool status = false;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    string query = @"delete from PE.holidays where ID_HOLIDAY = :Holiday_id";
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("Holiday_id", OracleDbType.Int32, holidayId, System.Data.ParameterDirection.Input));

                        try
                        {
                            command.Connection.Open();
                            command.ExecuteNonQuery();
                            command.Connection.Close();
                        }
                        catch (OracleException ex)
                        {
                            Console.WriteLine(ex.ToString());
                            throw;
                        }
                        status = true;
                    }
                }
            }
            catch (Exception xe)
            {
                throw;
            }
            return status;
        }


        public bool CreateHoliday(Holiday holiday)
        {
            bool status = false;
            DateTime date = new DateTime();
            //establish a connection
            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    string query = @"INSERT INTO HOLIDAYS 
                                        ( HOLIDAY_DAY,
                                          DESCRIPTION) 
                                VALUES
                                    (:Holiday,
                                     :Description)";

                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("Holiday", holiday.InsertDay ));
                        command.Parameters.Add(new OracleParameter("Description", holiday.Description));


                        try
                        {
                            command.Connection.Open();
                            command.ExecuteNonQuery();
                            command.Connection.Close();
                        }
                        catch (OracleException ex)
                        {
                            Console.WriteLine(ex.ToString());
                            throw;
                        }
                        status = true;
                    }
                }
            }
            catch (Exception xe)
            {
                throw;
            }
            return status;
        }


        public List<Holiday> GetAllHolidays()
        {
            List<Holiday> holidays;
            Holiday holiday;

            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string selectAllHolidays = @"SELECT  
                                                ID_HOLIDAY, 
                                                HOLIDAY_DAY,
                                                DESCRIPTION FROM HOLIDAYS";

                    using (OracleCommand command = new OracleCommand(selectAllHolidays, db))
                    {

                        OracleDataReader reader = command.ExecuteReader();
                        holidays = new List<Holiday>();
                        while (reader.Read())
                        {
                            holiday = new Holiday();
                            holiday.HolidayId = Convert.ToInt32(reader["ID_HOLIDAY"]);
                            holiday.Day = Convert.ToDateTime(reader["HOLIDAY_DAY"]);
                            holiday.Description = Convert.ToString(reader["DESCRIPTION"]);
                            holiday.InsertDay = holiday.Day.ToShortDateString();
                            holidays.Add(holiday);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return holidays;
        }

         public Holiday GetHoliday(int holidayId)
        {
            Holiday holiday = new Holiday() ;
           try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string selectAllHolidays = @"SELECT  
                                                ID_HOLIDAY, 
                                                HOLIDAY_DAY,
                                                DESCRIPTION FROM HOLIDAYS where ID_HOLIDAY = :Holiday_id";

                    using (OracleCommand command = new OracleCommand(selectAllHolidays, db))
                    {
                        command.Parameters.Add(new OracleParameter("Holiday_id", OracleDbType.Int32, holidayId, System.Data.ParameterDirection.Input));


                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {                        
                            holiday.HolidayId = Convert.ToInt32(reader["ID_HOLIDAY"]);
                            holiday.Day = Convert.ToDateTime(reader["HOLIDAY_DAY"]);
                            holiday.Description = Convert.ToString(reader["DESCRIPTION"]);
                            holiday.InsertDay = holiday.Day.ToShortDateString();
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return holiday;
        }
    }
}