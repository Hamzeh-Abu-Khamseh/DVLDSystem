using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PeopleBusinessLayer
{

    public class clsManageDetainedLicenses
    {
        public enum enMode { AddNew=0,Update=1};
        public enMode Mode =enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsers CreatedByUserInfo;
        public bool IsReleased  { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUsers ReleasedByUserInfo;
        public int ReleaseApplicationID { get; set; }

        public clsManageDetainedLicenses()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = -1;
            CreatedByUserID = -1;
            IsReleased = false;
            ReleaseDate = DateTime.Now;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;
            Mode = enMode.AddNew;

        }
        public clsManageDetainedLicenses(int detainLicenseID, int licenseID, DateTime detainDate,
            float fineFees, int createdByUserID, bool isReleased,
            DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            this.DetainID = detainLicenseID;
            this.LicenseID = licenseID;
            this.DetainDate = detainDate;
            this.FineFees = fineFees;
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUsers.FindByUserID(this.CreatedByUserID);
            this.IsReleased = isReleased;
            this.ReleaseDate = releaseDate;
            this.ReleasedByUserID = releasedByUserID;
            this.ReleasedByUserInfo = clsUsers.FindByUserID(this.ReleasedByUserID);
            this.ReleaseApplicationID = releaseApplicationID;

            Mode = enMode.Update;

        }
        public static clsManageDetainedLicenses FindDetainedLicenseByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.Now;
            float FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Now;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;
            if(clsManageDetainedLicensesDataAccess.FindDetainedLicenseByLicenseID(LicenseID, ref DetainID, ref DetainDate, ref FineFees,
                ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsManageDetainedLicenses(DetainID, LicenseID, DetainDate,
                    FineFees, CreatedByUserID, IsReleased,
                    ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
                return null;
        }

        public static clsManageDetainedLicenses FindDetainedLicenseByDetainID(int DetainID)
        {

            int LicenseID = -1;
            DateTime DetainDate = DateTime.Now;
            float FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.Now;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;
            if (clsManageDetainedLicensesDataAccess.FindDetainedLicenseByDetainID(DetainID, ref LicenseID, ref DetainDate, ref FineFees,
                ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsManageDetainedLicenses(DetainID, LicenseID, DetainDate,
                    FineFees, CreatedByUserID, IsReleased,
                    ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
                return null;
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsManageDetainedLicensesDataAccess.DetainLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);

            return (this.DetainID !=0);
        }

        private bool _UpdateDetainedLicense()
        {
            return clsManageDetainedLicensesDataAccess.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsManageDetainedLicensesDataAccess.IsLicenseDetained(LicenseID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateDetainedLicense();

            }
            return false;
            
        }
        public static DataTable GetAllDetainedLicenses()
        {
            return clsManageDetainedLicensesDataAccess.GetDetainedLicenses();
        }       
        public static int DetainLicense(int LicenseID, DateTime DetainDate, float FineFees,
            int CreatedByUserID)
        {
            return clsManageDetainedLicensesDataAccess.DetainLicense(LicenseID, DetainDate, FineFees, CreatedByUserID);
        }

        public  bool ReleaseDetainedLicense(int ReleasedByUserID,int ApplicationID)
        {
            return clsManageDetainedLicensesDataAccess.ReleaseLicense(this.DetainID ,ReleasedByUserID,ApplicationID);
        }

     
        
    }
}
