using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace PES.Services
{
    public class PEService
    {
        private PESDBContext dbContext = new PESDBContext();
        private EmployeeService _employeeService;

        public PEService()
        {
            _employeeService = new EmployeeService();
        }


        public bool InsertPE(PEs pe)
        {
            bool status = false;

            // Connect to the DB 
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string insertQuery = @"INSERT INTO PE (EVALUATION_PERIOD,
                                                       ID_EMPLOYEE,
                                                       ID_EVALUATOR,
                                                       ID_STATUS,
                                                       TOTAL,
                                                       ENGLISH_SCORE,
                                                       PERFORMANCE_SCORE,
                                                       COMPETENCE_SCORE,
                                                       EVALUATION_YEAR,
                                                       ID_PERIOD,
                                                       STATUS,
                                                       ""RANK""
                                                      ) 
                                               VALUES (:evaluationPeriod,
                                                       :employeeId,
                                                       :evaluatorId,
                                                       :statusId,
                                                       :total,
                                                       :englishScore,
                                                       :performanceScore,
                                                       :competenceScore,
                                                       :evaluationYear,
                                                       :periodId,
                                                       :status,
                                                       :rank)";

                // Adding parameters
                using (OracleCommand command = new OracleCommand(insertQuery, db))
                {
                    command.Parameters.Add(new OracleParameter("evaluationPeriod", OracleDbType.Date, pe.EvaluationPeriod, ParameterDirection.Input));
                    command.Parameters.Add(new OracleParameter("employeeId", pe.EmployeeId));
                    command.Parameters.Add(new OracleParameter("evaluatorId", pe.EvaluatorId));
                    command.Parameters.Add(new OracleParameter("statusId", pe.StatusId));
                    command.Parameters.Add(new OracleParameter("total", pe.Total));
                    command.Parameters.Add(new OracleParameter("englishScore", pe.EnglishScore));
                    command.Parameters.Add(new OracleParameter("performanceScore", pe.PerformanceScore));
                    command.Parameters.Add(new OracleParameter("competenceScore", pe.CompeteneceScore));
                    command.Parameters.Add(new OracleParameter("evaluationYear", pe.EvaluationYear));
                    command.Parameters.Add(new OracleParameter("periodId", pe.PeriodId));
                    command.Parameters.Add(new OracleParameter("periodId", 'Y'));
                    command.Parameters.Add(new OracleParameter("rank", pe.Rank));

                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                        status = true;
                    }
                    catch (Exception xe)
                    {
                        throw;
                    }

                    return status;
                }
            }
        }
        
        public PEs GetPerformanceEvaluationByDate(int userId, DateTime date)
        {
            // complete function
            PEs PES = null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT ID_PE," +
                                            "EVALUATION_PERIOD," +
                                            "ID_EMPLOYEE," +
                                            "ID_EVALUATOR," +
                                            "ID_STATUS," +
                                            "TOTAL," +
                                            "ENGLISH_SCORE," +
                                            "PERFORMANCE_SCORE," +
                                            "COMPETENCE_SCORE," +
                                            "\"RANK\" " +
                                            "FROM PE WHERE ID_EMPLOYEE = " + userId + " AND EVALUATION_PERIOD = TO_DATE('"+ date.ToString("MM/dd/yyyy") +"', 'MM/DD/YYYY') AND ROWNUM <=1 " +
                                            "ORDER BY EVALUATION_PERIOD, ID_PE DESC";

                    OracleCommand Command = new OracleCommand(Query, db);
                    OracleDataReader Read = Command.ExecuteReader();
                    PES = new PEs();
                    while (Read.Read())
                    {

                        // Store data in employee object 
                        PEs pes = new PEs();
                        pes.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);
                        pes.PEId = Convert.ToInt32(Read["ID_PE"]);
                        pes.EvaluationPeriod = Convert.ToDateTime(Read["EVALUATION_PERIOD"]);
                        pes.EvaluatorId = Convert.ToInt32(Read["ID_EVALUATOR"]);
                        pes.StatusId = Convert.ToInt32(Read["ID_STATUS"]);
                        pes.Total = Convert.ToDouble(Read["TOTAL"]);
                        string englishScore = Convert.ToString(Read["ENGLISH_SCORE"]);
                        if (!string.IsNullOrEmpty(englishScore))
                        {
                            pes.EnglishScore = Convert.ToDouble(englishScore);
                        }
                        else
                        {
                            pes.EnglishScore = 0;
                        }
                        pes.PerformanceScore = Convert.ToDouble(Read["PERFORMANCE_SCORE"]);
                        pes.CompeteneceScore = Convert.ToDouble(Read["COMPETENCE_SCORE"]);
                        string rank = Convert.ToString(Read["RANK"]);
                        if (!string.IsNullOrEmpty(rank))
                        {
                            pes.Rank = double.Parse(rank);
                        }
                        else
                        {
                            pes.Rank = null;
                        }

                        PES = pes;
                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return PES;

        }

        public PEs GetPerformanceEvaluationByDateEmail(string email, DateTime date) 
        {
            // Get user
            var user = _employeeService.GetByEmail(email);

            PEs pe = GetPerformanceEvaluationByDate(user.EmployeeId, date);

            return pe;
        }

        public List<PEs> GetPerformanceEvaluationByUserID(int userid) 
        {
            List<PEs> listPES = null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT ID_PE," +
                                           "EVALUATION_PERIOD," +
                                           "ID_EMPLOYEE," +
                                           "ID_EVALUATOR," +
                                           "ID_STATUS," +
                                           "TOTAL," +
                                           "ENGLISH_SCORE," +
                                           "PERFORMANCE_SCORE," +
                                           "COMPETENCE_SCORE," +
                                           "EVALUATION_YEAR," +
                                           "ID_PERIOD," +
                                           "\"RANK\"" +
                                           "FROM PE WHERE ID_EMPLOYEE = '" + userid + "'AND ID_STATUS = 1 ORDER BY ID_PE DESC"; // correct status by joining with status table
                                           //check why this query had rownum <=1
                   

                    OracleCommand Comand = new OracleCommand(Query, db);
                    
                    OracleDataReader Read = Comand.ExecuteReader();
                    listPES = new List<PEs>();
                    while (Read.Read())
                    {

                        // Store data in employee object 
                        PEs pes = new PEs();
                        pes.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);
                        pes.PEId = Convert.ToInt32(Read["ID_PE"]);
                        pes.EvaluationPeriod = Convert.ToDateTime(Read["EVALUATION_PERIOD"]);
                        pes.EvaluatorId = Convert.ToInt32(Read["ID_EVALUATOR"]);
                        pes.StatusId = Convert.ToInt32(Read["ID_STATUS"]);
                        pes.Total = Convert.ToDouble(Read["TOTAL"]);
                        pes.PerformanceScore = Convert.ToDouble(Read["PERFORMANCE_SCORE"]);
                        pes.CompeteneceScore = Convert.ToDouble(Read["COMPETENCE_SCORE"]);
                        pes.EvaluationYear = Convert.ToInt32(Read["EVALUATION_YEAR"]);
                        pes.PeriodId = Convert.ToInt32(Read["ID_PERIOD"]);

                        string rank = Convert.ToString(Read["RANK"]);
                        if (!string.IsNullOrEmpty(rank))
                        {
                            pes.Rank = double.Parse(rank);
                        }
                        else 
                        {
                            pes.Rank = null;
                        }
                        string englishScore = Convert.ToString(Read["ENGLISH_SCORE"]);
                        if (!string.IsNullOrEmpty(englishScore))
                        {
                            pes.EnglishScore = Convert.ToDouble(englishScore);
                        }
                        else 
                        {
                            pes.EnglishScore = 0;
                        }

                        listPES.Add(pes);
                    }
                    
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }
            return listPES;
        }

        //public PEs GetPerformanceEvaluationByUser(int userId)
        //{
        //    return null;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="peId">Performance Evaluation Id</param>
        /// <returns>Returns a performance evaluation</returns>
        public PEs GetPerformanceEvaluationById(int peId) 
        {
            PEs pe = new PEs();

            // Connect to the DB 
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string insertQuery = @" SELECT ID_PE, 
                                            EVALUATION_PERIOD,
                                            ID_EMPLOYEE,
                                            ID_EVALUATOR,
                                            ID_STATUS,
                                            TOTAL,
                                            ENGLISH_SCORE,
                                            PERFORMANCE_SCORE,
                                            COMPETENCE_SCORE,
                                            RANK 
                                     FROM PE 
                                     WHERE ID_PE = :peId AND ROWNUM <=1
                                     ORDER BY EVALUATION_PERIOD, ID_PE DESC";

                // Adding parameters
                using (OracleCommand command = new OracleCommand(insertQuery, db))
                {
                    command.Parameters.Add(new OracleParameter("peId", peId));

                    try
                    {
                        command.Connection.Open();
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {

                            // Store data in employee object 
                            pe = new PEs();
                            pe.EmployeeId = Convert.ToInt32(reader["ID_EMPLOYEE"]);
                            pe.PEId = Convert.ToInt32(reader["ID_PE"]);
                            pe.EvaluationPeriod = Convert.ToDateTime(reader["EVALUATION_PERIOD"]);
                            pe.EvaluatorId = Convert.ToInt32(reader["ID_EVALUATOR"]);
                            pe.StatusId = Convert.ToInt32(reader["ID_STATUS"]);
                            pe.Total = Convert.ToDouble(reader["TOTAL"]);
                            pe.EnglishScore = Convert.ToDouble(reader["ENGLISH_SCORE"]);//DISCOMMENTED
                            pe.PerformanceScore = Convert.ToDouble(reader["PERFORMANCE_SCORE"]);
                            pe.CompeteneceScore = Convert.ToDouble(reader["COMPETENCE_SCORE"]);
                            pe.Rank = Convert.ToDouble(reader["RANK"]);//DISCOMMENTED
                            string rank = Convert.ToString(reader["RANK"]);
                            if (!string.IsNullOrEmpty(rank))
                            {
                                pe.Rank = double.Parse(rank);
                            }
                            else
                            {
                                pe.Rank = null;
                            }
                            string englishScore = Convert.ToString(reader["ENGLISH_SCORE"]);
                            if (!string.IsNullOrEmpty(englishScore))
                            {
                                pe.EnglishScore = Convert.ToDouble(englishScore);
                            }
                            else
                            {
                                pe.EnglishScore = 0;
                            }

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
            return pe;
        }

        public List<PerformanceSectionHelper> GetPerformanceEvaluationByIDPE(int peId)
        {
            List<PerformanceSectionHelper> listPerformanceSectionHelp = null;

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string selectQuery = @"SELECT T.TITLE AS TITLE,
                                                  ST.SUBTITLE AS SUBTITLE,
                                                  D.DESCRIPTION AS DESCRIPTION,
                                                  SC.SCORE_EMPLOYEE AS SCEMPLOYEe, 
                                                  SC.SCORE_EVALUATOR AS SCEVALUATOR, 
                                                  SC.COMMENTS AS COMMENTS, 
                                                  SC.CALCULATION AS CALCULATION
                                             FROM TITLE T
                                       INNER JOIN SUBTITLE ST ON T.ID_TITLE = ST.ID_TITLE
                                       INNER JOIN DESCRIPTION D ON ST.ID_SUBTITLE = D.ID_SUBTITLE
                                        LEFT JOIN SCORE SC ON D.ID_DESCRIPTION = SC.ID_DESCRIPTION
                                            WHERE SC.ID_PE = :peId 
                                         ORDER BY T.ID_TITLE, ST.ID_SUBTITLE, D.ID_DESCRIPTION";
                    using (OracleCommand command = new OracleCommand(selectQuery, db))
                    {
                        command.Parameters.Add(new OracleParameter("peId", peId));
                        OracleDataReader reader = command.ExecuteReader();
                        listPerformanceSectionHelp = new List<PerformanceSectionHelper>();                     
                        while (reader.Read())
                        {
                            var performanceSectionHelp = new PerformanceSectionHelper();

                            performanceSectionHelp.Title = Convert.ToString(reader["TITLE"]);
                            performanceSectionHelp.Subtitle = Convert.ToString(reader["SUBTITLE"]);
                            performanceSectionHelp.Description = Convert.ToString(reader["DESCRIPTION"]);
                            performanceSectionHelp.ScoreEmployee = Convert.ToInt32(reader["SCEMPLOYEE"]);
                            performanceSectionHelp.ScoreEvaluator = Convert.ToInt32(reader["SCEVALUATOR"]); 
                            performanceSectionHelp.Comments = Convert.ToString(reader["COMMENTS"]);
                            performanceSectionHelp.Calculation = Convert.ToDouble(reader["CALCULATION"]);

                            listPerformanceSectionHelp.Add(performanceSectionHelp);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;   
            }
            return listPerformanceSectionHelp;
        }

        public bool UpdateRank(int peId, double rank)
        {
            bool status = false;

            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string updateRank = @"UPDATE PE
                                        SET ""RANK"" = :rank
                                       WHERE ID_PE = :peId";

                using (OracleCommand command = new OracleCommand(updateRank, db))
                {
                    command.Parameters.Add(new OracleParameter("rank", rank));
                    command.Parameters.Add(new OracleParameter("peId", peId));

                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                        status = true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }
                }
            }
            return status;
        }

        public int VerifyPE(int employee, int evaluator, int period, int year)
        {
            var PE = 0;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string insertQuery = @" SELECT ID_PE 
                                     FROM PE 
                                     WHERE ID_EMPLOYEE = :employeeId AND 
                                     ID_EVALUATOR = :evaluatorId AND
                                     ID_PERIOD = :periodId AND 
                                     EVALUATION_YEAR = :evalYear AND
                                     STATUS != :status";

                // Adding parameters
                using (OracleCommand command = new OracleCommand(insertQuery, db))
                {
                    command.Parameters.Add(new OracleParameter("employeeId", employee));
                    command.Parameters.Add(new OracleParameter("evaluatorId", evaluator));
                    command.Parameters.Add(new OracleParameter("periodId", period));
                    command.Parameters.Add(new OracleParameter("evalYear", year));
                    command.Parameters.Add(new OracleParameter("status", "Disable"));

                    try
                    {
                        command.Connection.Open();
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            PE = Convert.ToInt32(reader["ID_PE"]);
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
            return PE;
        }

        public bool UpdateStatus(int peId)
        {
            bool status = false;

            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string updateRank = @"UPDATE PE
                                        SET STATUS = 'N'
                                       WHERE ID_PE = :peId";

                using (OracleCommand command = new OracleCommand(updateRank, db))
                {
                    command.Parameters.Add(new OracleParameter("peId", peId));

                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }
                }
            }
            return status;
        }

        public List<Period> GetAllPeriods()
        {
            List<Period> listPeriods = null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT UNIQUE ID_PERIOD " +
                                           "FROM PE WHERE ID_STATUS = 'Y' ORDER BY ID_PERIOD ASC";

                    OracleCommand Comand = new OracleCommand(Query, db);

                    OracleDataReader Read = Comand.ExecuteReader();
                    listPeriods = new List<Period>();
                    while (Read.Read())
                    {

                        // Store data in employee object 
                        Period period = new Period();
                        period.PeriodId = Convert.ToInt32(Read["ID_PERIOD"]);
                        if(period.PeriodId == 1)
                        {
                            period.Name = "JANUARY - JUNE";
                        }
                        else
                        {
                            period.Name = "JULY - DECEMBER";
                        }

                        listPeriods.Add(period);
                    }

                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }
            return listPeriods;
        }

        public List<int> GetAllYears()
        {
            List<int> listYears = null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT UNIQUE EVALUATION_YEAR " +
                                           "FROM PE WHERE ID_STATUS = 'Y' ORDER BY EVALUATION_YEAR DESC";

                    OracleCommand Comand = new OracleCommand(Query, db);

                    OracleDataReader Read = Comand.ExecuteReader();
                    listYears = new List<int>();
                    while (Read.Read())
                    {

                        // Store data in employee object 
                        int year;
                        year = Convert.ToInt32(Read["EVALUATION_YEAR"]);

                        listYears.Add(year);
                    }

                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }
            return listYears;
        }
    }
}