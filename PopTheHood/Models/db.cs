using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Models
{
    public class db
    {

        public static IConfiguration configuration
        {
            get;
            private set;
        }
        public db(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        public static string GetConnectionString()
        {
            return configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
        }

        //SqlConnection con;

        //public db()
        //{
        //    var configuration = GetConfiguration();
        //    con = new SqlConnection(configuration.GetSection("Data").GetSection("ConnectionString").Value);
        //}

        //public IConfigurationRoot GetConfiguration()
        //{
        //    var builder = new ConfigurationBinder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        //    return builder.Build();
        //}
    }
}
