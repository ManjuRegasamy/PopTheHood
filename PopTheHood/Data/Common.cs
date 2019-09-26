﻿using Microsoft.ApplicationBlocks.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Data
{
    public class Common
    {

        //public static string GetConnString()
        //{
        //    IConfigurationBuilder builder = new ConfigurationBuilder();
        //    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

        //    var root = builder.Build();
        //    var Providers = root.GetConnectionString("DefaultConnection");
        //    string ConString = Providers.ToString();
        //    return ConString;
        //}


        #region DBCon
        public static IConfiguration configuration
        {
            get;
            private set;
        }
        public Common(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        public static string GetConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            IConfigurationRoot configuration = builder.Build();
            var connstring = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            return connstring;
        }
        #endregion


        public static string SaveErrorLog(string FunctionName, string ErrorMessage)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@FunctionName", FunctionName));
                parameters.Add(new SqlParameter("@ErrorMessage", ErrorMessage));
                
                string rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spSaveErrorLog", parameters.ToArray()).ToString();
                return rowsAffected;
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }
        }

    }
}