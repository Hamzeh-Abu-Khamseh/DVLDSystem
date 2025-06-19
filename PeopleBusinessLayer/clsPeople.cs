using DataAccessLayer;
using System;
using System.Data;

namespace PeopleBusinessLayer
{
    public class clsPeople
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        public clsCountry CountryInfo;

        public clsPeople()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";

            this.Mode = enMode.AddNew;
        }

        public clsPeople(int personID, string nationalNo, string firstName,
            string secondName, string thirdName, string lastName,
            DateTime dateOfBirth, short gendor, string address,
            string phone, string email, int nationalityCountryID, string imagePath)
        {
            this.PersonID = personID;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gendor = gendor;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.ImagePath = imagePath;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPeopleDataAccess.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID,
                this.ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPeopleDataAccess.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email,
                this.NationalityCountryID, this.ImagePath);
        }

        public static clsPeople Find(int PersonID)
        {
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";
            if (clsPeopleDataAccess.GetPersonInfoByPersonID(PersonID, ref NationalNo, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor,
                ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName,
                    ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email,
                    NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static clsPeople Find(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";
            if (clsPeopleDataAccess.GetPersonInfoByNationalID(NationalNo, ref PersonID, ref FirstName,
                ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor,
                ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName,
                    ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email,
                    NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static DataTable GetPeople()
        {
            return clsPeopleDataAccess.GetPeople();
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleDataAccess.DeletePerson(PersonID);
        }

        public static bool IsPersonExist(int PersonID)
        {
            return clsPeopleDataAccess.IsPersonExist(PersonID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPeopleDataAccess.IsPersonExistByNationalNo(NationalNo);
        }

        public static string GetNationalNoByDriverID(int DriverID)
        {
            return clsPeopleDataAccess.GetNationalNoByDriverID(DriverID);
        }
    }
}