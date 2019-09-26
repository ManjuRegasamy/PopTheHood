using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Data
{
    public class Payment
    {

        public static DataTable GetVehiclePaymentDetails(int UserId, string Search)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                //Create the parameters in the SqlParameter array
                //SqlParameter[] parameters =
                //{
                //    new SqlParameter("@UserId", SqlDbType.Int, 10, ParameterDirection.Input, false, 10, 0, "UserId", DataRowVersion.Proposed, UserId),
                //    new SqlParameter("@Search", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 10, 0, "Search", DataRowVersion.Proposed, Search)
                //};
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", UserId));
                parameters.Add(new SqlParameter("@Search", Search));
               

                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetVehiclePaymentDetails", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
