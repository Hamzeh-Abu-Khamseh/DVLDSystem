using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsManageTestsDataAccess
    {
        public static bool GetTestInfoByTestID(int TestID,ref int TestAppointmentID,ref bool TestResult,ref string Notes,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select * from Tests where TestID=@TestID";

            SqlCommand command =new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    TestResult = Convert.ToBoolean(reader["TestResult"]);

                    if (reader["Notes"] == DBNull.Value)

                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                }
                else
                    isFound = false;

                reader.Close();
            }
            catch(Exception e)
            {
                clsErrorsLogging.LogError("Error in GetTestInfoByTestID: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }
        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
           (int PersonID, int LicenseClassID, int TestTypeID, ref int TestID,
             ref int TestAppointmentID, ref bool TestResult,
             ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
                FROM            LocalDrivingLicenseApplications INNER JOIN
                                         Tests INNER JOIN
                                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                WHERE        (Applications.ApplicantPersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    
                    IsFound = true;
                    TestID = Convert.ToInt32(reader["TestID"]);
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    if (reader["Notes"] == DBNull.Value)

                        Notes = "";
                    else
                        Notes = reader["Notes"].ToString();

                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                }
                else
                {                  
                    IsFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in GetLastTestByPersonAndTestTypeAndLicenseClass: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static int AddNewTest( int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID) VALUES
                 (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                 
                   Update TestAppointments set IsLocked=1 where TestAppointmentID=@TestAppointmentID ;
                    select scope_Identity();";

            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID=insertedID;
                }
            }
            catch(Exception e)
            {
                clsErrorsLogging.LogError("Error in AddNewTest: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return TestID;

        }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update Tests set TestAppointmentID=@TestAppointmentID,
                            TestResult=@TestResult,
                            Notes=@Notes, CreatedByUserID=@CreatedByUserID";

            SqlCommand command=new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                clsErrorsLogging.LogError("Error in UpdateTest: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
                RowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }
            return (RowsAffected > 0);
        }

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from Tests";
            
            SqlCommand command=new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch(Exception e)
            {
                clsErrorsLogging.LogError("Error in GetAllTests: " + e.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT PassedTestCount = count(TestTypeID)
                         FROM Tests INNER JOIN
                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
						 where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestResult=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
                }
            }

            catch (Exception ex)
            {
                clsErrorsLogging.LogError("Error in GetPassedTestCount: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return PassedTestCount;




        }
    }


}
