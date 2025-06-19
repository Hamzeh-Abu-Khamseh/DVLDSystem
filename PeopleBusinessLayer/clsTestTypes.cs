using DataAccessLayer;
using System.Data;
using System.Runtime.CompilerServices;

namespace PeopleBusinessLayer
{
    public class clsTestTypes
    {
        public enum enMode { AddNew=0, Update=1 };
        public static enMode Mode= enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public clsTestTypes.enTestType TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }

        public static DataTable GetAllTestTypes()
        {
            return DataAccessLayer.clsManageTestTypesDataAccess.GetAllTestTypes();
        }
        public clsTestTypes()
        {
            this.TestTypeID = clsTestTypes.enTestType.VisionTest;
            TestTypeDescription = "";
            TestTypeTitle = "";
            TestTypeFees = 0;
            Mode = enMode.AddNew;
        }
        public clsTestTypes(clsTestTypes.enTestType TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeFees = TestTypeFees;
            this.TestTypeDescription = TestTypeDescription;
            Mode=enMode.Update; 
        }

        public static clsTestTypes Find(clsTestTypes.enTestType TestTypeID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            float TestTypeFees = 0;
            if (clsManageTestTypesDataAccess.GetTestTypeInformationByID((int)TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
                return null;

        }
   
        private  bool _AddNewTestType()
        {
            this.TestTypeID=  (clsTestTypes.enTestType)clsManageTestTypesDataAccess.AddNewTestType(this.TestTypeTitle, this.TestTypeDescription,this.TestTypeFees);
            return (this.TestTypeTitle !="");
        }
        private  bool _UpdateTestType()
        {
            return clsManageTestTypesDataAccess.UpdateTestType((int)this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateTestType();
            }
            return false;
        }
    }
}
