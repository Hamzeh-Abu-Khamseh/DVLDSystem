using DataAccessLayer;
using System;
using System.Data;
using System.Security.Policy;
using System.Windows.Forms;

namespace PeopleBusinessLayer
{
    public class clsTestAppointments
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int TestAppointmentID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsTestTypes.enTestType TestTypeID { get ; set; }
        public clsTestTypes TestType
        {
            get { return clsTestTypes.Find(TestTypeID); }
        }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplications RetakeTestApplicationInfo {  get; set; }

        public int TestID
        {
            get { return _GetTestID(); }
        }

        public clsTestAppointments()
        {
            this.Mode = enMode.AddNew;
            this.TestTypeID = clsTestTypes.enTestType.VisionTest;
            this.TestAppointmentID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
        }
        public clsTestAppointments(int TestAppointmentID,clsTestTypes.enTestType TestTypeID, int LocalDrivingLicenseApplicationID , DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.TestTypeID = TestTypeID;
            this.AppointmentDate = AppointmentDate;
            this.CreatedByUserID=CreatedByUserID;
            this.PaidFees = PaidFees;
            this.IsLocked = IsLocked;

            Mode = enMode.Update;
        }

       
       
        public static clsTestAppointments Find(int TestAppointmentID)
        {
            int TestTypeID = 1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            if (clsTestAppointmentsDataAccess.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,ref AppointmentDate,ref PaidFees,ref CreatedByUserID,ref IsLocked))
            {
                return new clsTestAppointments(TestAppointmentID, (clsTestTypes.enTestType)TestTypeID,
                    LocalDrivingLicenseApplicationID, AppointmentDate,PaidFees, CreatedByUserID, IsLocked);
            }
            else
            {
                return null;
            }
        }

        public static clsTestAppointments GetLastTestAppointment(int LocalDrivingLicenseApplicationID,clsTestTypes.enTestType TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;


            if (clsTestAppointmentsDataAccess.GetLastTestAppointmentInfo(LocalDrivingLicenseApplicationID, (int)TestTypeID, ref TestAppointmentID
                , ref AppointmentDate, ref PaidFees,ref CreatedByUserID,ref  IsLocked))
            {
                return new clsTestAppointments(TestAppointmentID, TestTypeID,
                    LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            }
            else
                return null;
        }

        public static DataTable GetAppointmentsWith(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentsDataAccess.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }
        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID= clsTestAppointmentsDataAccess.AddNewTestAppointment(this.LocalDrivingLicenseApplicationID, (int)this.TestTypeID, this.AppointmentDate
                , this.PaidFees, this.CreatedByUserID, this.IsLocked);

            return this.TestAppointmentID != -1;
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentsDataAccess.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID
                , this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:

                    {
                        return _UpdateTestAppointment();
                    }
            }
            return false;
        }

        private int _GetTestID()
        {
            return clsTestAppointmentsDataAccess.GetTestID(TestAppointmentID);
        }

    }
}
