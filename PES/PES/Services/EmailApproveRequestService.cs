using Oracle.ManagedDataAccess.Client;
using PES.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Net;
using PES.ViewModels;
using System.Web.Routing;

namespace PES.Services
{
    public class EmailApproveRequestService
    {
        private PESDBContext dbContext = new PESDBContext();
    
       
        private SmtpClient client;
        public EmailApproveRequestService()
        {
            dbContext = new PESDBContext();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            client = new SmtpClient("smtp-mail.outlook.com");
        }

        public bool SendEmail(string email, string subject, string bodyMessage)
        {
                   
            MailMessage msje = new MailMessage();
            msje.From = new MailAddress(Globals.SMTPOutlookEmail);
            //msje.To.Add(ManagerEmail);
            msje.To.Add(email);
            msje.Subject = subject;
            msje.Body = bodyMessage;
            client.Port = 587;

            client.Credentials = new NetworkCredential(Globals.SMTPOutlookEmail, Globals.SMTPOutlookPass);
            client.EnableSsl = true;
            try
            {
                this.client.Send(msje);
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

        public bool SendEmails(List<string> emails, string subject, string bodyMessage)
        {
            foreach (var email in emails)
            {
                this.SendEmail(email, subject, bodyMessage);
            }

            return true;
        }

        public List<StatusRequestViewModel> GetDataRequest(int HeaderRequestId)
        {
            List<StatusRequestViewModel> dataRequests;
            StatusRequestViewModel data;

            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                                string query = @"with abc as (SELECT REPLAY_COMMENT , EMAIL , id_manager
                                                FROM PE.VACATION_HEADER_REQ 
                                                INNER JOIN  PE.EMPLOYEE ON  VACATION_HEADER_REQ.ID_EMPLOYEE = EMPLOYEE.ID_EMPLOYEE 
                                                WHERE ID_HEADER_REQ = '" + HeaderRequestId + "' ) " +
                                                "Select abc.REPLAY_COMMENT, abc.EMAIL, emp.EMAIL as \"managerEmail\" " +
                                                "From PE.EMPLOYEE emp, abc " +
                                                "where emp.id_employee = abc.id_manager ";  



                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        OracleDataReader reader = command.ExecuteReader();
                        dataRequests = new List<StatusRequestViewModel>();
                        while (reader.Read())
                        {
                            data = new StatusRequestViewModel();
                            data.Reason = Convert.ToString(reader["REPLAY_COMMENT"]);
                            data.EmployeeEmail = Convert.ToString(reader["EMAIL"]);
                            data.ManagerEmail = Convert.ToString(reader["managerEmail"]);

                            dataRequests.Add(data);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return dataRequests;
        }

        public bool ChangeRequestStatus(int headerRequestId, string approveReason)
        {

            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string query = @"UPDATE   PE.VACATION_HEADER_REQ 
                                                  SET     ID_REQ_STATUS = 3, 
                                                          REPLAY_COMMENT =  '"+ approveReason  + "' " +

                                                          "WHERE    ID_HEADER_REQ = '" + headerRequestId + "'" ;

                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        OracleDataReader reader = command.ExecuteReader();

                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return true;
        }
        //public bool LessNoVacDays(string employeeEmail, int noVacDays)
        //{
        //    try
        //    {

        //        using (OracleConnection db = dbContext.GetDBConnection())
        //        {
        //            db.Open();

        //            string query = @" UPDATE   PE.EMPLOYEE 
        //                              SET     FREE_DAYS = (FREE_DAYS + :noVacDays ) WHERE   EMAIL = '" + employeeEmail + "' ";

        //            using (OracleCommand command = new OracleCommand(query, db))
        //            {
        //                command.Parameters.Add(new OracleParameter("noVacDays", (noVacDays * -1)));
        //                OracleDataReader reader = command.ExecuteReader();
        //            }
        //            db.Close();
        //        }
        //    }
        //    catch (Exception xe)
        //    {
        //        throw;
        //    }

        //    return true;
        //}
    }
}