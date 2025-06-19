using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsApplicationTypesDataAccess
    {
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dtApplicationTypes = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query="SELECT * FROM ApplicationTypes";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtApplicationTypes.Load(reader);
                }
            }
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in GetAllApplicationTypes: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dtApplicationTypes;
          
        }
        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID,ref string ApplicationTypeTitle,ref float ApplicationFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID=@ApplicationTypeID";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                if(reader.Read())
                {
                    ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                    ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in GetApplicationTypeInfoByID: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool UpdateApplicationType(int ApplicationTypeID,string ApplicationTypeTitle,float ApplicationFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE ApplicationTypes SET ApplicationTypeTitle=@ApplicationTypeTitle," +
                "ApplicationFees=@ApplicationFees WHERE ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in UpdateApplicationType: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0; 
        }
        public static string GetApplicationTypeWith(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID=@ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                }
                else
                {
                    ApplicationTypeTitle = "";
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in GetApplicationTypeWith: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return ApplicationTypeTitle;
        }
        public static int AddNewApplicationType(string ApplicationTypeTitle, float ApplicationFees)
        {
            int ApplictionTypeID=-1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Insert Into ApplicationTypes (ApplicationTypeTitle,ApplicationFees) values
                            (@ApplicationTypeTitle,@ApplicationFees)
                              Select scope_Identity();";

            SqlCommand command= new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int InstertedID))
                {
                    ApplictionTypeID = InstertedID;
                }
            }
            catch(Exception ex)
            {
                clsErrorsLogging.LogError("Error in AddNewApplicationType: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return ApplictionTypeID;




        }
    }
}
