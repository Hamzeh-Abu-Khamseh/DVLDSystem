using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer
{
    public class clsManageDriversDataAccess
    {
        public static DataTable GetDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection= new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query= "SELECT * FROM Drivers_View";
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
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in GetDrivers: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dt;

        }

      
        public static int AddNewDriver(int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            int DriverID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate) VALUES (@PersonID, @CreatedByUserID, @CreatedDate); SELECT SCOPE_IDENTITY();";
            
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();
                object result= command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
                }
            }
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in AddNewDriver: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                DriverID = -1;
            }
            finally
            {
                connection.Close();
            }
            return DriverID;
            
        }

        public static bool UpdateDriver(int DriverID,int PersonID,int CreatedByUserID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update Driver set PersonID=@PersonID,CreatedByUserID=@CreatedByUserID where 
                            DriverID=@DriverID";
            SqlCommand command=new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();


            }
            catch(Exception e)
            {
                clsErrorsLogging.LogError("Error in UpdateDriver: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
                RowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }

        public static bool FindDriverByDriverID(int DriverID,ref int PersonID,ref int CreatedByUserID,ref DateTime CreatedDate)
        {
            bool IsFound=false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * from Drivers where DriverID=@DriverID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                }
                else
                    IsFound = false;

            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in FindDriverByDriverID: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static bool FindDriverByPersonID(int PersonID,ref int DriverID,ref int CreatedByUserID,ref DateTime CreatedDate)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * from Drivers where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);

                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch(Exception e)
            {
                clsErrorsLogging.LogError("Error in FindDriverByPersonID: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
                IsFound = false;
            }
            finally
            { 
                connection.Close();
            }
            return IsFound;
        }

        public static bool DoesDriverExistBy(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select DriverID from Drivers where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();

            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in DoesDriverExistBy: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
    }
}
