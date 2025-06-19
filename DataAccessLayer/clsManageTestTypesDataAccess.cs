using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsManageTestTypesDataAccess
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt=new DataTable(); 

            SqlConnection connection=new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * from TestTypes";

            SqlCommand command = new SqlCommand(query, connection);

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
                clsErrorsLogging.LogError("Error in GetAllTestTypes: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        
        }
        public static bool GetTestTypeInformationByID(int TestTypeID, ref string TestTypeTitle,ref string TestTypeDescription, ref float TestTypeFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * from TestTypes where TestTypeID=@TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeTitle = reader["TestTypeTitle"].ToString();
                    TestTypeDescription = reader["TestTypeDescription"].ToString();
                    TestTypeFees = Convert.ToSingle(reader["TestTypeFees"].ToString());
                    
                }
            }
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in GetTestTypeInformationByID: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound; ;
        }

        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Update TestTypes set TestTypeTitle=@TestTypeTitle,TestTypeDescription=@TestTypeDescription," +
                              "TestTypeFees=@TestTypeFees where TestTypeID=@TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in UpdateTestType: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0) ;
        }

        public static int AddNewTestType(string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            int TestTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Insert into TestTypes (TestTypeTitle,TestTypeDescription,TestTypeFees,) Values(@TestTypeTitle,@TestTypeDescription,@TestTypeFees)
                            select Scope_Identity();";
            SqlCommand command= new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestTypeID = insertedID;
                }
                
            }
            catch(Exception e)
            {
                clsErrorsLogging.LogError("Error in AddNewTestType: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return TestTypeID;

        }

        
    }
}
