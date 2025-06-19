using DataAccessLayer;
using System;
using System.Data;

namespace PeopleBusinessLayer
{

    public class clsManageDrivers
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public int DriverID { get; set; }
        public int PersonID { get; set; }

        public clsPeople PersonInfo;
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public clsManageDrivers()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }
        public clsManageDrivers(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = clsPeople.Find(PersonID);
            Mode = enMode.Update;
        }        
        public static DataTable GetDrivers()
        {
            return clsManageDriversDataAccess.GetDrivers();
        }
        public static clsManageDrivers FindDriverByDriverID(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsManageDriversDataAccess.FindDriverByDriverID(DriverID, ref  PersonID,ref  CreatedByUserID, ref CreatedDate))
            {
                return new clsManageDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
                return null;
        }
        public static clsManageDrivers FindDriverByPersonID(int PersonID)
        {
            int DriverID=-1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsManageDriversDataAccess.FindDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
                return new clsManageDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }
       
        private bool _AddNewDriver()
        {
            this.DriverID = clsManageDriversDataAccess.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return (this.DriverID != 0);
        }
        private bool _UpdateDriver()
        {
            return clsManageDriversDataAccess.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }
        
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateDriver();
            }
            return false;
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return clsManageLicenses.GetDriverLicenses(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalDrivingLicensesDataAccess.GetAllInternationalLicensesBy(DriverID);
        }

    }
}
