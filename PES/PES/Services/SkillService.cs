using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class SkillService
    {
        private PESDBContext dbContext = new PESDBContext();


        public bool InsertSkill(Skill skill)
        {
            bool status = false;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string InsertSkill = "INSERT INTO SKILL (SKILL)" +
                                            "VALUES ('" + skill.Description + "')";
                    OracleCommand Command = new OracleCommand(InsertSkill, db);
                    Command.ExecuteNonQuery();

                    status = true;
                    db.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return status;
        }
    }
}