using DataAccessLayer;
using System.Data;
using System.Runtime.CompilerServices;

namespace PeopleBusinessLayer
{
    public class clsManageTests
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public clsTestAppointments TestAppointmentInfo { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }

        public clsManageTests()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        public clsManageTests(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointments.Find(TestAppointmentID);

            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        public static clsManageTests Find(int TestID)
        {

            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            if (clsManageTestsDataAccess.GetTestInfoByTestID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsManageTests(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);

            }
            else
                return null;
        }


        public static clsManageTests GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, clsTestTypes.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;
            if (clsManageTestsDataAccess.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID, LicenseClassID, (int)TestTypeID, ref TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsManageTests(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
                return null;
        }
        public static DataTable GetAllTests()
        {
            return clsManageTestsDataAccess.GetAllTests();
        }
        public static byte GetPassedTestCount(int LocalDrivingLicesneApplictionID)
        {
            return clsManageTestsDataAccess.GetPassedTestCount(LocalDrivingLicesneApplictionID);
        }

        public static bool DidPassAllTests(int LocalDrivingLIcenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLIcenseApplicationID) == 3;
        }

        private bool _AddNewTest()
        {
            this.TestID = clsManageTestsDataAccess.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return (this.TestID != -1);
        }
        private bool _UpdateTest()
        {
            return clsManageTestsDataAccess.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewTest())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    return _UpdateTest();
            }
            return false;
        }
    }
}

