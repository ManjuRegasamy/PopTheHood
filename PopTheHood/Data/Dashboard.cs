using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Data
{
    public class Dashboard
    {

        public static DataSet GetDashboard()
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                //Create the parameters in the SqlParameter array
                //List<SqlParameter> parameters = new List<SqlParameter>();
                //parameters.Add(new SqlParameter("@UserId", UserId));

                //Execute the query
                using (DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetDashboard"))
                {
                    return ds;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
