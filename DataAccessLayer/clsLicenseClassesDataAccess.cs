using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataAccessLayer
{
    public class clsLicenseClassesDataAccess
    {
        public static bool GetClassInfoByClassName(string ClassName, ref int LicenseClassID, ref string ClassDescription,
            ref int MinimumAllowedAge, ref int DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT * FROM LicenseClasses WHERE ClassName=@ClassName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    ClassDescription = reader["ClassDescription"].ToString();
                    MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);


                }
            }
            catch (Exception ex)
            {
               clsErrorsLogging.LogError("Error in GetClassInfoByClassName: " + ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetClassInfoByClassID(int LicenseClassID, ref string ClassName, ref string ClassDescription,
            ref int MinimumAllowedAge, ref int DefaultValidityLength, ref float ClassFees)
        {

            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * from LicenseClasses where LicenseClassID=@LicenseClassID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ClassName = reader["ClassName"].ToString();
                    ClassDescription = reader["ClassDescription"].ToString();
                    MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in GetClassInfoByClassID: " + e.Message, EventLogEntryType.Error);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;

        }

        public static DataTable GetAllLicenseClasses()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT ClassName FROM LicenseClasses";
            SqlCommand command = new SqlCommand(query, connection);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in GetAllLicenseClasses: " + ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int AddNewLicenseClass(string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float ClassFees)
        {
            int LicenseClassID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Insert into LicenseClasses (ClassName,ClassDescription,MinimumAllowedAge,DefaultValidityLength,ClassFees)
                        values (@ClassName,@ClassDescription,@MinimumAllowedAge,@DefaultValidityLength,@ClassFees );
                        Select Scope_Identity();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    LicenseClassID = InsertedID;
                }

            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in AddNewLicenseClass: " + e.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();

            }
            return LicenseClassID;
        }

        public static bool UpdateLicenseClass(int LicenseClassID, string ClassName, string ClassDescription,
             int MinimumAllowedAge, int DefaultValidityLength, float ClassFees)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update LicenseClass set ClassName=@ClassName, ClassDescription=@ClassDescription
                            , MinimumAllowedAge=@MinimumAllowedAge,DefaultValidityLength=@DefaultValidityLength,ClassFees=@ClassFees where
                                LicenseClassID=@LicenseClassID";

            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in UpdateLicenseClass: " + e.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }
    }
}
