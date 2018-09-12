using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
namespace PES.Services
{
    public class ProfileService
    {
        private PESDBContext dbContext;

        public ProfileService()
        {
            dbContext = new PESDBContext();
        }

        // Get from table PROFILES the profile name by ID
        public Profile GetProfileByID(int profileId)
        {
            Profile profile;

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string selectProfiles = @"SELECT ID_PROFILE,
                                                     PROFILE
                                                FROM PROFILE
                                               WHERE ID_PROFILE = :profileId";
                    using (OracleCommand command = new OracleCommand(selectProfiles, db))
                    {
                        command.Parameters.Add(new OracleParameter("profileID", profileId));
                        OracleDataReader reader = command.ExecuteReader();
                        profile = new Profile();
                        while (reader.Read())
                        {
                            profile.ProfileId = Convert.ToInt32(reader["ID_PROFILE"]);
                            profile.Name = Convert.ToString(reader["PROFILE"]);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return profile;
        }

        // Get from table PROFILE all the profiles 
        public List<Profile> GetAllProfiles()
        {
            List<Profile> profiles;
            Profile profile;

            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string selectAllProfiles = @"SELECT ID_PROFILE,
                                                     PROFILE
                                                FROM PROFILE";
                    using (OracleCommand command = new OracleCommand(selectAllProfiles, db))
                    {
                        OracleDataReader reader = command.ExecuteReader();
                        profiles = new List<Profile>();

                        while (reader.Read())
                        {
                            profile = new Profile();
                            profile.ProfileId = Convert.ToInt32(reader["ID_PROFILE"]);
                            profile.Name = Convert.ToString(reader["PROFILE"]);
                            profiles.Add(profile);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return profiles;
        }

    }
}