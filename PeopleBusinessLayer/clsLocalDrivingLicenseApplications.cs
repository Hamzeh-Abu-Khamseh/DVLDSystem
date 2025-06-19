using DataAccessLayer;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace PeopleBusinessLayer
{
    public class clsLocalDrivingLicenseApplications :clsApplications
    {
        public enum enMode { AddNew=0,Update=1 }
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClasses LicenseClassInfo { get; set; }
        public string PersonFullName
        {
            get { return base.ApplicantFullName; }
        }

        public clsLocalDrivingLicenseApplications()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID=-1;

            Mode=enMode.AddNew;
        }


        public clsLocalDrivingLicenseApplications(int localDrivingLicenseApplicationID,int applicationID, int applicantPersonID,
            DateTime applicationDate,int applicationTypeID,enApplicationStatus ApplicationStatus,DateTime lastStatusDate,
            float paidFees,int createdByUserID,int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.ApplicationID = applicationID;
            this.ApplicantPersonID= applicantPersonID;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID; 
            this.ApplicationStatus= ApplicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees=paidFees;
            this.CreatedByUserID= createdByUserID;
            this.LicenseClassID= LicenseClassID;
            this.LicenseClassInfo = clsLicenseClasses.FindLicenseClass(LicenseClassID);

            Mode = enMode.Update;
            

        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsDataAccess.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID,
                this.ApplicationID, this.LicenseClassID);
        }

        public static clsLocalDrivingLicenseApplications FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID=-1;
            int LicenseClassID=-1;

            if (clsLocalDrivingLicenseApplicationsDataAccess.GetLocalDrivingApplicationByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID,
                ref ApplicationID,ref LicenseClassID))
            {
                clsApplications Application = clsApplications.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, (enApplicationStatus)Application.ApplicationStatus,
                    Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
            {
                return null;
            }
        }

        public static clsLocalDrivingLicenseApplications FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            int LicenseClassID= -1;

            if(clsLocalDrivingLicenseApplicationsDataAccess.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID,ref LicenseClassID))
            {
                clsApplications Application = clsApplications.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID, Application.ApplicationID,
                 Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID,(enApplicationStatus) Application.ApplicationStatus,
                 Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
                
            }
            else
            {
                return null;
            }
        }
        
        public bool Save()
        {
            //because of inheritance we first need to take care of saving the application then we save the Local one. 
            base.Mode = (clsApplications.enMode)Mode;
            if (!base.Save())
                return false;


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
            }
            return false;
        }
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.GetAllLocalDrivingLicenseApplications();
        }

        public  bool Delete()
        {
            //first we need to delete the LocalDrivingLicenseApplication then we can delete the Application
            bool IsLocalDrivingLicenseApplicationDeleted = false;
            bool IsApplicationDeleted = false;

            IsLocalDrivingLicenseApplicationDeleted= clsLocalDrivingLicenseApplicationsDataAccess.DeleteLocalDrivingLicenseApplication(this.ApplicationID);
            if (!IsLocalDrivingLicenseApplicationDeleted)
                return false;

            IsApplicationDeleted = base.Delete();

            return IsApplicationDeleted;
        }

        public bool DidPassTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DidPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        
        public static bool DidPassTestType(int LocalDrivingLicenseApplicationID,clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DidPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DidAttendTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.DidAttendTestType(this.LocalDrivingLicenseApplicationID,(int) TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID,(int) TestTypeID);
        }
        public clsManageTests GetLastTestPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsManageTests.GetLastTestByPersonAndTestTypeAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.TotalTrialsPerTest(LocalDrivingLicenseApplicationID,(int)TestTypeID);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID,clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID,(int)TestTypeID); 
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int) TestTypeID);
        }

        //public static clsManageTests GetLastTestPerTestType(clsTestTypes.enTestType TestTypeID)
        //{
        //    return clsManageTests.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        //}

        public byte GetPassedTestCount()
        {
            return clsManageTests.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsManageTests.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

       public bool DidPassAllTests()
        {
            return clsManageTests.DidPassAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool DidPassAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsManageTests.DidPassAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirstTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;
            clsManageDrivers Driver = clsManageDrivers.FindDriverByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsManageDrivers();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;

                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                    return -1;
            }

            else
            {
                DriverID = Driver.DriverID;
            }

            clsManageLicenses License = new clsManageLicenses();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.Paidfees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsManageLicenses.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                
                this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }
        public int GetActiveLicenseID()
        {
            return clsManageLicenses.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }






    }
}
