﻿using System;
using System.Configuration;


namespace DataAccessLayer
{
    static class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DVLDConnectionString"].ConnectionString;

    }
}
