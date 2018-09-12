using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using PES.DBContext;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class LM_SkillService
    {
        private PESDBContext dbContext = new PESDBContext();

        public bool InsertLM_Skill(LM_Skill lm_skill)
        {
            bool status = false;

            try
            {
                using(OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string InsertQuery = "INSERT INTO LM_SKILL (ID_SKILL," +
                                                                "ID_PE," +
                                                                "CHECK_EMPLOYEE," +
                                                                "CHECK_EVALUATOR)" +
                                         "VALUES (" + lm_skill.SkillId + "," +
                                                     lm_skill.PEId + ", '" +
                                                     lm_skill.CheckEmployee + "', '" +
                                                     lm_skill.CheckEvaluator + "')";
                    OracleCommand Command = new OracleCommand(InsertQuery, db);
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

        public List<LM_Skill> GetSkillsBypeID(int peID)
        {
            List<LM_Skill> skills = new List<LM_Skill>();
            LM_Skill skill = new LM_Skill();

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string SelectSkill = @"SELECT ID_LMSKILL,
                                                ID_SKILL,
                                                ID_PE, 
                                                CHECK_EMPLOYEE,
                                                CHECK_EVALUATOR 
                                                FROM LM_SKILL WHERE ID_PE = :peID";
                    using (OracleCommand Command = new OracleCommand(SelectSkill, db))
                    {
                        Command.Parameters.Add(new OracleParameter("peID", peID));
                        Command.ExecuteReader();
                        OracleDataReader Reader = Command.ExecuteReader();
                        while (Reader.Read())
                        {
                            skill = new LM_Skill();
                            skill.LMSkillId = Convert.ToInt32(Reader["ID_LMSKILL"]);
                            skill.SkillId = Convert.ToInt32(Reader["ID_SKILL"]);
                            skill.PEId = Convert.ToInt32(Reader["ID_PE"]);
                            skill.CheckEmployee = Convert.ToString(Reader["CHECK_EMPLOYEE"]);
                            skill.CheckEvaluator = Convert.ToString(Reader["CHECK_EVALUATOR"]);
                            skills.Add(skill);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return skills;
        }

        public List<SkillHelper> GetSkillsWithNameByPEId(int peId) 
        {
            List<SkillHelper> skills = new List<SkillHelper>();
            SkillHelper skill = new SkillHelper();

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string selectskill = @"SELECT SKILL.SKILL, 
                                                LM_SKILL.ID_PE,
                                                LM_SKILL.CHECK_EMPLOYEE, 
                                                LM_SKILL.CHECK_EVALUATOR
                                                FROM LM_SKILL
                                                INNER JOIN SKILL ON SKILL.ID_SKILL = LM_SKILL.ID_SKILL
                                                WHERE LM_SKILL.ID_PE = :peID
                                                ORDER BY SKILL.ID_SKILL ASC";
                    using (OracleCommand Command = new OracleCommand(selectskill, db))
                    {
                        Command.Parameters.Add(new OracleParameter("peID", peId));
                        Command.ExecuteReader();
                        OracleDataReader Reader = Command.ExecuteReader();
                        while (Reader.Read())
                        {
                            skill = new SkillHelper();
                            skill.SkillName = Convert.ToString(Reader["SKILL"]);
                            skill.PEId = Convert.ToInt32(Reader["ID_PE"]);
                            skill.CheckEmployee = Convert.ToString(Reader["CHECK_EMPLOYEE"]);
                            skill.CheckEvaluator = Convert.ToString(Reader["CHECK_EVALUATOR"]);                        
                            skills.Add(skill);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return skills;
        }
    }
}