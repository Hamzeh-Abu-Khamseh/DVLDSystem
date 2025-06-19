using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace PeopleBusinessLayer
{
    public class clsManageInternationalLicenses:clsApplications
    {
        public enum enMode { AddNew=0,Update=1};
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public clsManageDrivers DriverInfo;
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }       
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
       

        public clsManageInternationalLicenses()
        {
            this.ApplicationTypeID = (int)clsApplications.enApplicationType.NewInternationalLicense;

            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive=true;
            Mode = enMode.AddNew;
        }

        public clsManageInternationalLicenses(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID,
             int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)

        {
           
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplications.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsManageDrivers.FindDriverByDriverID(this.DriverID);

            Mode = enMode.Update;
        }
        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalDrivingLicensesDataAccess.AddNewInternationalLicense(this.ApplicationID,
                this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate,
                this.IsActive, this.CreatedByUserID);

            return this.InternationalLicenseID != -1;
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalDrivingLicensesDataAccess.UpdateInternationalLicense(this.InternationalLicenseID,
                this.ApplicantPersonID, this.DriverID, this.IssuedUsingLocalLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }
        public static DataTable GetInternationalLicenses()
        {
            return clsInternationalDrivingLicensesDataAccess.GetInternationalLicenses();
        }
       

        public static clsManageInternationalLicenses Find(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1; int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true; int CreatedByUserID = 1;

            if (clsInternationalDrivingLicensesDataAccess.GetInternationalLicenseInfo(InternationalLicenseID, ref ApplicationID, ref DriverID,
                ref IssuedUsingLocalLicenseID,
            ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                //now we find the base application
                clsApplications Application = clsApplications.FindBaseApplication(ApplicationID);


                return new clsManageInternationalLicenses(Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID,
                                     InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID,
                                         IssueDate, ExpirationDate, IsActive);

            }

            else
                return null;
        }
        
        public bool Save()
        {

            base.Mode = (clsApplications.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateInternationalLicense();

            }
            return false;

        }
        public static int GetActiveInternationalLicenseID(int DriverID)
        {
            return clsInternationalDrivingLicensesDataAccess.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }
        public static DataTable GetAllInternationalLicensesBy(int DriverID)
        {
            return clsInternationalDrivingLicensesDataAccess.GetAllInternationalLicensesBy(DriverID);
        }
    }
}
