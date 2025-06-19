using DataAccessLayer;
using System.Data;
namespace PeopleBusinessLayer
{
    public class clsApplicationTypes
    {
        enum enMode { AddNew = 0, Update = 1 };
        enMode Mode = enMode.AddNew;

        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationFees { get; set; }
        public clsApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            Mode = enMode.Update;
        }
        public clsApplicationTypes()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = "";
            ApplicationFees = 0;
            Mode = enMode.AddNew;

        }
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesDataAccess.GetAllApplicationTypes();
        }
        public static clsApplicationTypes Find(int ApplicationTypeID)
        {

            string ApplicationTypeTitle = "";
            float ApplicationFees = 0;
            if (clsApplicationTypesDataAccess.GetApplicationTypeInfoByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
                return null;
        }

        private bool _AddNewApplicationType()
        {
            this.ApplicationTypeID = clsApplicationTypesDataAccess.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);
            return (this.ApplicationTypeID != -1);
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesDataAccess.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateApplicationType();
            }
            return false;
        }
    }
}
