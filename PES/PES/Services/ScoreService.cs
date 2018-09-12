using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using PES.DBContext;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace PES.Services
{
    public class ScoreService
    {
        private PESDBContext dbContext;
        public ScoreService()
        {
            dbContext = new PESDBContext();
        }
        public bool InsertScore (Score score)
        {
            bool status= false;
            
                    using (OracleConnection db = dbContext.GetDBConnection())
                    {

                string insertQuery = @" INSERT INTO SCORE (ID_DESCRIPTION,
                                                                 ID_PE, 
                                                                 SCORE_EMPLOYEE, 
                                                                 SCORE_EVALUATOR, 
                                                                 COMMENTS, 
                                                                 CALCULATION) 
                                                VALUES (:DescriptionId, 
                                                      :PEId, 
                                                      :ScoreEmployee, 
                                                      :ScoreEvaluator, 
                                                      :Comments, 
                                                      :Calculation)";

                using (OracleCommand Command = new OracleCommand(insertQuery, db))
                {
                    Command.Parameters.Add(new OracleParameter("DescriptionId", score.DescriptionId));
                    Command.Parameters.Add(new OracleParameter("PEId", score.PEId));
                    Command.Parameters.Add(new OracleParameter("ScoreEmployee", score.ScoreEmployee));
                    Command.Parameters.Add(new OracleParameter("ScoreEvaluator", score.ScoreEvaluator));
                    Command.Parameters.Add(new OracleParameter("Comments", score.Comments));
                    Command.Parameters.Add(new OracleParameter("Calculation", score.Calculation));

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

        public List<Score> GetPEScoresbyPEID(int peID)
        {
            List<Score> scores = new List<Score>();
            Score score = new Score();

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string SelectScores = "SELECT ID_SCORE, " +
                                                  "ID_DESCRIPTION, " +
                                                  "ID_PE, " +
                                                  "SCORE_EMPLOYEE, " +
                                                  "SCORE_EVALUATOR, " +
                                                  "COMMENTS, " +
                                                  "CALCULATION " +
                                                  "FROM SCORE WHERE ID_PE = " + peID;
                    OracleCommand Command = new OracleCommand(SelectScores, db);
                    Command.ExecuteReader();
                    OracleDataReader Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        score = new Score();
                        score.ScoreId = Convert.ToInt32(Reader["ID_SCORE"]);
                        score.DescriptionId = Convert.ToInt32(Reader["ID_DESCRIPTION"]);
                        score.PEId = Convert.ToInt32(Reader["ID_PE"]);
                        score.ScoreEmployee = Convert.ToInt32(Reader["SCORE_EMPLOYEE"]);
                        score.ScoreEvaluator = Convert.ToInt32(Reader["SCORE_EVALUATOR"]);
                        score.Comments = Convert.ToString(Reader["COMMENTS"]);
                        score.Calculation = Convert.ToInt32(Reader["CALCULATION"]);
                        scores.Add(score);
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return scores;
        }

        public Score GetPEScorebyPEIdDescId(int peID, int DescID)
        {
            Score score = new Score();

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string selectScores = "SELECT ID_SCORE, " +
                                                   "ID_DESCRIPTION, " +
                                                   "ID_PE, " +
                                                   "SCORE_EMPLOYEE, " +
                                                   "SCORE_EVALUATOR, " +
                                                   "COMMENTS, " +
                                                   "CALCULATION " +
                                                   "FROM SCORE WHERE ID_PE = " + peID + "AND ID_DESCRIPTION = " + DescID;
                    OracleCommand Command = new OracleCommand(selectScores, db);
                    Command.ExecuteReader();
                    OracleDataReader Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        score = new Score();
                        score.ScoreId = Convert.ToInt32(Reader["ID_SCORE"]);
                        score.DescriptionId = Convert.ToInt32(Reader["ID_DESCRIPTION"]);
                        score.PEId = Convert.ToInt32(Reader["ID_PE"]);
                        score.ScoreEmployee = Convert.ToInt32(Reader["SCORE_EMPLOYEE"]);
                        score.ScoreEvaluator = Convert.ToInt32(Reader["SCORE_EVALUATOR"]);
                        score.Comments = Convert.ToString(Reader["COMMENTS"]);
                        score.Calculation = Convert.ToInt32(Reader["CALCULATION"]);
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return score;
        }
    }
}