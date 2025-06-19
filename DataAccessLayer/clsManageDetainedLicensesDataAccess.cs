using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsManageDetainedLicensesDataAccess
    {
        public static DataTable GetDetainedLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT DetainedLicenses.* ,People.NationalNo, People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName+' '+People.LastName 
                AS FullName FROM DetainedLicenses  INNER JOIN Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID 
                INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID 
                INNER JOIN People ON Drivers.PersonID = People.PersonID ";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in GetDetainedLicenses: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        
        public static bool FindDetainedLicenseByLicenseID(int LicenseID,ref int DetainID,ref DateTime DetainDate,ref float FineFees,
            ref int CreatedBy,ref bool IsReleased,ref DateTime ReleaseDate,ref int ReleasedByUserID,ref int ReleaseApplicationID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select top 1 * from DetainedLicenses where LicenseID=@LicenseID order by DetainID desc";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
          

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.Read())
                {
                    DetainID = Convert.ToInt32(reader["DetainID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedBy = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);
                    if (reader["ReleaseDate"] != DBNull.Value)
                    {
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                    }
                    else
                    {
                        ReleaseDate = DateTime.Now;
                    }
                    if (reader["ReleasedByUserID"] != DBNull.Value)
                    {
                        ReleasedByUserID = Convert.ToInt32(reader["ReleasedByUserID"]);
                    }
                    else
                    {
                        ReleasedByUserID = -1;
                    }
                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                    {
                        ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);
                    }
                    else
                    {
                        ReleaseApplicationID = -1;
                    }
                    result = true;
                    reader.Close();
                }
               
            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in FindDetainedLicenseByLicenseID: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public static bool FindDetainedLicenseByDetainID(int DetainID, ref int LicenseID, ref DateTime DetainDate,
           ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate,
           ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT top 1 * FROM DetainedLicenses WHERE DetainID = @DetainID order by DetainID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {


                    isFound = true;

                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                    if (reader["ReleaseDate"] == DBNull.Value)

                        ReleaseDate = DateTime.MaxValue;
                    else
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);


                    if (reader["ReleasedByUserID"] == DBNull.Value)

                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = Convert.ToInt32(reader["ReleasedByUserID"]);

                    if (reader["ReleaseApplicationID"] == DBNull.Value)

                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);

                }
                else
                {

                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in FindDetainedLicenseByDetainID: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int DetainLicense(int LicenseID, DateTime DetainDate, float FineFees,
            int CreatedByUserID)
        {
            int DetainID = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into DetainedLicenses(LicenseID,DetainDate,FineFees,CreatedByUserID,IsReleased) 
                            values(@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,0)
                               select scope_Identity();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result= command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DetainID = insertedID;
                }
                
            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in DetainLicense: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return DetainID;
        }

        public static bool UpdateDetainedLicense(int DetainID,
            int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"update DetainedLicenses
                              SET LicenseID = @LicenseID, 
                              DetainDate = @DetainDate, 
                              FineFees = @FineFees,
                              CreatedByUserID = @CreatedByUserID,   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainedLicenseID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                clsErrorsLogging.LogError("Error in UpdateDetainedLicense: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

       

        public static bool ReleaseLicense(int DetainID,int ReleasedByUserID, int ReleaseApplicationID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update DetainedLicenses set IsReleased=1,ReleaseDate=@ReleaseDate,
                            ReleasedByUserID=@ReleasedByUserID,ReleaseApplicationID=@ReleaseApplicationID where DetainID=@DetainID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);


            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    result = true;
                }
                
            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in ReleaseLicense: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public static bool IsLicenseDetained(int LicenseID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select IsDetained=1 from DetainedLicenses where LicenseID=@LicenseID and IsReleased=0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    result = true;
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in IsLicenseDetained: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}
