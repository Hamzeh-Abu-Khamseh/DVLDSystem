using DataAccessLayer;
using System;
using System.Data;
namespace PeopleBusinessLayer
{
    public class clsManageLicenses
    {
        public enum enMode { AddNew=0,Update=1 };
        public enMode Mode =enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public clsManageDrivers DriverInfo;
        public int LicenseClassID { get; set; }
        public clsLicenseClasses LicenseClassInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float Paidfees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }

        public string IssueReasonText
        {
            get { return GetIssueReasonText(this.IssueReason); }
        }
        public int CreatedByUserID { get; set; }

        public clsManageDetainedLicenses DetainedInfo { get; set; }

        public bool IsDetained
        {
            get { return clsManageDetainedLicenses.IsLicenseDetained(this.LicenseID); }
        }


        public clsManageLicenses()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.Paidfees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsManageLicenses(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate,
            string Notes, float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.DriverInfo = clsManageDrivers.FindDriverByDriverID(DriverID);
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClasses.FindLicenseClass(this.LicenseClassID); 
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.Paidfees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.DetainedInfo = clsManageDetainedLicenses.FindDetainedLicenseByLicenseID(this.LicenseID);

            Mode = enMode.Update;
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsManageLicensesDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes, this.Paidfees, this.IsActive, (short)this.IssueReason, this.CreatedByUserID);
            return this.LicenseID != 0;
        }
        private bool _UpdateLicense()
        {
            return clsManageLicensesDataAccess.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID,
                this.LicenseClassID, this.IssueDate, this.ExpirationDate, this.Notes,
                this.Paidfees, this.IsActive, (short)this.IssueReason, this.CreatedByUserID);
        }


        public static clsManageLicenses FindLicenseByApplicationID(int ApplicationID)
        {
            int licenseID = -1;
            int driverID = -1;
            int licenseClass = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = "";
            float paidFees = 0;
            bool isActive = false;
            short issueReason = 0;
            int createdByUserID = -1;
            if (clsManageLicensesDataAccess.GetLicenseInfoByApplicationID(ApplicationID, ref licenseID, ref driverID, ref licenseClass, ref issueDate
                , ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID))
            {
                return new clsManageLicenses(licenseID, ApplicationID, driverID, licenseClass, issueDate, expirationDate, notes,
                    paidFees, isActive,(enIssueReason) issueReason, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsManageLicenses FindLicenseByLicenseID(int LicenseID)
        {
            int ApplicationID = -1;
            int driverID = -1;
            int licenseClass = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = "";
            float paidFees = 0;
            bool isActive = false;
            short issueReason = 0;
            int createdByUserID = -1;
            if (clsManageLicensesDataAccess.GetLicenseInfoByLiceneseID(LicenseID, ref ApplicationID, ref driverID, ref licenseClass, ref issueDate
                , ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID))
            {
                return new clsManageLicenses(LicenseID, ApplicationID, driverID, licenseClass, issueDate, expirationDate, notes,
                    paidFees, isActive, (enIssueReason)issueReason, createdByUserID);
            }
            else
            {
                return null;
            }

        }

        public static DataTable GetAllLicenses()
        {
            return clsManageLicensesDataAccess.GetAllLicenses();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLicense())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                            return false;
                    }
                case enMode.Update:
                    return _UpdateLicense();
            }
            return false;
        }

        public static bool DoesLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsManageLicensesDataAccess.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsManageLicensesDataAccess.GetAllLicenesByDriverID(DriverID);
        }
        //is License Exist
        public static DataTable GetAllLicenesByDriverID(int DriverID)
        {

            return clsManageLicensesDataAccess.GetAllLicenesByDriverID(DriverID);
        }

        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public static bool DeactivateLicense(int LicenseID)
        {
            return clsManageLicensesDataAccess.DeactivateLicense(LicenseID);
        }

        public bool DeactivateCurrentLicense()
        {
            return clsManageLicensesDataAccess.DeactivateLicense(this.LicenseID);
        }
       
        public string GetIssueReasonText(enIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.LostReplacement:
                    return "Replacment for lost";
                case enIssueReason.DamagedReplacement:
                    return "Replacment for damaged";
                case enIssueReason.Renew:
                    return "Renew";
                default:
                    return "FirstTime";
            }
                    
        }

        public int DetainLicense(float FineFees,int CreatedByUserID)
        {
          
            clsManageDetainedLicenses DetainedLicense = new clsManageDetainedLicenses();
            DetainedLicense.LicenseID = this.LicenseID;
            DetainedLicense.DetainDate = DateTime.Now;
            DetainedLicense.FineFees = FineFees;
            DetainedLicense.CreatedByUserID = CreatedByUserID;

            if (!DetainedLicense.Save())
            {
                return -1;
            }
            return DetainedLicense.LicenseID;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID,ref int ApplicationID)
        {
           
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID=this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID=(int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            Application.LastStatusDate=DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find((int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }
            ApplicationID = Application.ApplicationID;

            
           return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID);

        }
        public clsManageLicenses RenewLicense(string Notes,int CreatedByUserID)
        {
            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplications.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find((int)clsApplications.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsManageLicenses NewLicense = new clsManageLicenses();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicenseClassInfo.DefaultValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.Paidfees = this.LicenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsManageLicenses.enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                return null;
            }

            
            DeactivateCurrentLicense();

            return NewLicense;
        }

        public clsManageLicenses Replace(enIssueReason IssueReason, int CreatedByUserID)
        {

            clsApplications Application = new clsApplications();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplications.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplications.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.Find(Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsManageLicenses NewLicense = new clsManageLicenses();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.Paidfees = 0;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;



            if (!NewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLicense();

            return NewLicense;
        }
    }
}
