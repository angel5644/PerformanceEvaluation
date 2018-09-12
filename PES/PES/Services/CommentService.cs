using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using System.IO;
using System.Data.SqlClient;
using System.Data;


namespace PES.Services
{
    public class CommentService
    {
        private PESDBContext dbContext = new PESDBContext();

        // Get Comments of the PE to the DB by peID
        public List<Comment> GetCommentByPE(int peId) 
        {
            List<Comment> Comments;
            Comment Comment;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string selectComments = @"SELECT ID_COMMENT, 
                                                    ID_PE, 
                                                    TRAINNING_EMPLOYEE, 
                                                    TRAINNING_EVALUATOR, 
                                                    ACKNOWLEDGE_EVALUATOR, 
                                                    ""comm/recomm_employee"",
                                                    ""comm/recomm_evaluator"" 
                                                    FROM ""COMMENT"" WHERE ID_PE = :peId";
                    using (OracleCommand command = new OracleCommand(selectComments, db))
                    {
                        command.Parameters.Add(new OracleParameter("peId", peId));
                        OracleDataReader reader = command.ExecuteReader();
                        Comments = new List<Comment>();
                        while (reader.Read())
                    {
                        Comment = new Comment();
                            Comment.CommentId = Convert.ToInt32(reader["ID_COMMENT"]);
                            Comment.PEId = Convert.ToInt32(reader["ID_PE"]);
                            Comment.TrainningEmployee = Convert.ToString(reader["TRAINNING_EMPLOYEE"]);
                            Comment.TrainningEvaluator = Convert.ToString(reader["TRAINNING_EVALUATOR"]);
                            Comment.AcknowledgeEvaluator = Convert.ToString(reader["ACKNOWLEDGE_EVALUATOR"]);
                            Comment.CommRecommEmployee = Convert.ToString(reader["comm/recomm_employee"]);
                            Comment.CommRecommEvaluator = Convert.ToString(reader["comm/recomm_evaluator"]);
                        Comments.Add(Comment);
                    }

                    }
                    db.Close();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return Comments;
        }

        public bool InsertComment(Comment comment)
        {
            bool status = false ;

            using (OracleConnection db = dbContext.GetDBConnection()) 
            {
                string InsertComment = @"INSERT INTO ""COMMENT"" (ID_PE, 
                                                             TRAINNING_EMPLOYEE, 
                                                             TRAINNING_EVALUATOR, 
                                                             ACKNOWLEDGE_EVALUATOR, 
                                                             ""comm/recomm_employee"", 
                                                             ""comm/recomm_evaluator"") 
                                            VALUES (:PEId,
                                                   :TrainningEmployee,
                                                   :TrainningEvaluator,
                                                   :AcknowledgeEvaluator,
                                                   :CommRecommEmployee,
                                                   :CommRecommEvaluator)";

                using (OracleCommand Command = new OracleCommand(InsertComment, db))
                {
                    Command.Parameters.Add(new OracleParameter("PEId", comment.PEId));
                    Command.Parameters.Add(new OracleParameter("TrainningEmployee", comment.TrainningEmployee));
                    Command.Parameters.Add(new OracleParameter("TrainningEvaluator", comment.TrainningEvaluator));
                    Command.Parameters.Add(new OracleParameter("AcknowledgeEvaluator", comment.AcknowledgeEvaluator));
                    Command.Parameters.Add(new OracleParameter("CommRecommEmployee", comment.CommRecommEmployee));
                    Command.Parameters.Add(new OracleParameter("CommRecommEvaluator", comment.CommRecommEvaluator));

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
    }
}