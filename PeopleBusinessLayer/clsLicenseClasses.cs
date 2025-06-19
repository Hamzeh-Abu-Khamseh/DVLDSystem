using DataAccessLayer;
using System.Data;

namespace PeopleBusinessLayer
{
    public class clsLicenseClasses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }

        public clsLicenseClasses()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge= -1;
            this.DefaultValidityLength = -1;
            this.ClassFees = -1;
            Mode=enMode.AddNew; 

        }

        public clsLicenseClasses(int LicenseClassID, string ClassName, string ClassDescription,
            int MinimumAllowedAge, int DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            Mode = enMode.Update;
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesDataAccess.GetAllLicenseClasses();
        }
        
        public static clsLicenseClasses FindLicenseClass(string ClassName)
        {
            int LicenseClassID = -1;
            string ClassDescription = "";
            int MinimumAllowedAge = -1;
            int DefaultValidityLength = -1;
            float ClassFees = -1;

            if (clsLicenseClassesDataAccess.GetClassInfoByClassName(ClassName, ref LicenseClassID, ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClasses(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }

        public static clsLicenseClasses FindLicenseClass(int LicenseClassID)
        {
            string ClassName = "";
            string ClassDescription = "";
            int MinimumAllowedAge = -1;
            int DefaultValidityLength = -1;
            float ClassFees = -1;
            if (clsLicenseClassesDataAccess.GetClassInfoByClassID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge,
                ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClasses(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }
        
        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassesDataAccess.AddNewLicenseClass(this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength
                , this.ClassFees);

            return this.LicenseClassID != -1;
        }
        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassesDataAccess.UpdateLicenseClass(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                            return _UpdateLicenseClass();

                        }
            return false;
        }
    }
}
