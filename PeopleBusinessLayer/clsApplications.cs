using DataAccessLayer;
using System;

namespace PeopleBusinessLayer
{
    public class clsApplications
    {
        public enum enMode { AddNew=0,Update=1 };

        public  enMode Mode =enMode.AddNew;

        public enum enApplicationType {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6,
                RetakeTest=7
        };
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPeople PersonInfo { get; set; }
        public string ApplicantFullName
        {
            get
            {
                return clsPeople.Find(ApplicantPersonID).FullName;
            }
        }
        public int ApplicationTypeID { get; set; }
        public clsApplicationTypes ApplicationTypeInfo { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusTest
        {
            get
            {
                switch(ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsers CreatedByUserInfo { get; set; }

        public clsApplications()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsApplications(int applicationID, int applicantPersonID, DateTime applicationDate, int applicationTypeID
            , enApplicationStatus applicationStatus, DateTime lastStatusDate, float paidFees, int createdByUserID)
        {
            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
            this.PersonInfo = clsPeople.Find(applicantPersonID);
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeInfo = clsApplicationTypes.Find(applicationTypeID);
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUsers.FindByUserID(createdByUserID);
            Mode = enMode.Update;

        }

        

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsDataAccess.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,(byte) this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (ApplicationID != -1);
        }
        private bool _UpdateApplication()
        {
            return clsApplicationsDataAccess.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
                (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public static clsApplications FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0; int CreatedByUserID = -1;

            bool IsFound = clsApplicationsDataAccess.GetApplicationInfo
                                (
                                    ApplicationID, ref ApplicantPersonID,
                                    ref ApplicationDate, ref ApplicationTypeID,
                                    ref ApplicationStatus, ref LastStatusDate,
                                    ref PaidFees, ref CreatedByUserID
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new clsApplications(ApplicationID, ApplicantPersonID,
                                     ApplicationDate, ApplicationTypeID,
                                    (enApplicationStatus)ApplicationStatus, LastStatusDate,
                                     PaidFees, CreatedByUserID);
            else
                return null;
        }

        public bool Cancel()
        {
            return clsApplicationsDataAccess.UpdateStatus(ApplicationID, (int)enApplicationStatus.Cancelled);
        }
        public bool SetComplete()
        {
            return clsApplicationsDataAccess.UpdateStatus(ApplicationID, (int)enApplicationStatus.Completed);
        }
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }

        public bool Delete()
        {
            return clsApplicationsDataAccess.DeleteApplication(this.ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID,int ApplicationTypeID)
        {
            return clsApplicationsDataAccess.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID,clsApplications.enApplicationType ApplicationTypeID)
        {
            return clsApplicationsDataAccess.GetActiveApplicationID(PersonID,(int) ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplications.enApplicationType ApplicationTypeID,int LicenseClassID)
        {
            return clsApplicationsDataAccess.GetActiveApplicationIDForLicenseClass(PersonID,(int) ApplicationTypeID, LicenseClassID);
        }


    }
}
