using DataAccessLayer;
using System.Collections.Generic;
using System.Data;

namespace PeopleBusinessLayer
{
    public class clsUsers
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPeople PersonInfo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUsers()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
            Mode = enMode.AddNew;
        }

        public clsUsers(int userID, int personID, string userName, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.PersonInfo = clsPeople.Find(personID);
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
            Mode = enMode.Update;
        }
        private bool _AddNewUser()
        {
            this.UserID = clsUsersDataAccess.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.UserID != -1);
        }
        private bool _UpdateUser()
        {
            return clsUsersDataAccess.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive);
        }


        public static bool DoesUserExistByPersonID(int PersonID)
        {
            return clsUsersDataAccess.CheckIfUserConnnectedToPerson(PersonID);
        }
        public static bool DoesUserExist(int UserID)
        {
            return clsUsersDataAccess.IsUserExist(UserID);
        }
        public static bool DoesUserExist(string UserName)
        {
            return clsUsersDataAccess.IsUserExist(UserName);
        }
        public static clsUsers FindByUserID(int userID)
        {

            int personID = -1;
            string userName = "";
            string password = "";
            bool isActive = false;

            if (clsUsersDataAccess.GetUserInfoByUserID(userID, ref personID, ref userName, ref password, ref isActive))
            {
                return new clsUsers(userID, personID, userName, password, isActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUsers FindByPersonID(int PersonID)
        {
            int userID = -1;
            string userName = "";
            string password = "";
            bool isActive = false;
            if (clsUsersDataAccess.GetUserInfoByPersonID(PersonID, ref userID, ref userName, ref password, ref isActive))
            {
                return new clsUsers(userID, PersonID, userName, password, isActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUsers FindByUsernameAndPassword(string UserName, string Password)
        {

            int userID = -1;
            int personID = -1;
            bool isActive = false;

            if (clsUsersDataAccess.GetUserInfoForLogin(UserName, Password, ref userID, ref personID, ref isActive))
            {
                return new clsUsers(userID, personID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }
        }
        public static DataTable FilterUsersBy(string Filter, string Parameter)
        {
            return clsUsersDataAccess.GetUsersWith(Filter, Parameter);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }
        public static DataTable GetUsers()
        {
            return clsUsersDataAccess.GetUsers();
        }
        public static bool DeleteUser(int userID)
        {
            return clsUsersDataAccess.DeleteUser(userID);
        }
        public static List<string> GetUserNames()
        {
            return clsUsersDataAccess.GetUserNames();
        }

        public static bool UpdateUserPassword(int UserID, string Password)
        {
            return clsUsersDataAccess.UpdatePassword(UserID, Password);
        }
    }

}
