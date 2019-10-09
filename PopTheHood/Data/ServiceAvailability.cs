using Microsoft.ApplicationBlocks.Data;
using PopTheHood.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Data
{
    public class ServiceAvailability
    {
        public static int SaveAvailableService(ServicesModel ServicesModel, string Action)
        {
            //SqlParameter[] parameters =
            //{
            //    new SqlParameter("@ServicePlanID", SqlDbType.Int, 10, ParameterDirection.Input, false, 10, 0, "ServicePlanID", DataRowVersion.Proposed, vehicleservicehistorydetails.ServicePlanID),
            //    new SqlParameter("@ServiceName", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 10, 0, "ServiceName", DataRowVersion.Proposed, vehicleservicehistorydetails.ServiceName),
            //    new SqlParameter("@Description", SqlDbType.NVarChar, max, ParameterDirection.Input, false, 10, 0, "Description", DataRowVersion.Proposed, vehicleservicehistorydetails.ServiceDescription),
            //    new SqlParameter("@Action", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 10, 0, "Action", DataRowVersion.Proposed, Action)
            //};

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ServicePlanID", ServicesModel.AvailableServiceID));
            parameters.Add(new SqlParameter("@ServiceName", ServicesModel.ServiceName));
            parameters.Add(new SqlParameter("@Description", ServicesModel.Description));
            parameters.Add(new SqlParameter("@Action", Action));

            try
            {
                string ConnectionString = Common.GetConnectionString();
                int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spSaveAvailableService", parameters.ToArray());
                return rowsAffected;
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }
        }

        public static DataTable GetAvailableService()
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetAvailableService").Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataTable GetAvailableServiceById(int AvailableServiceID)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ServicePlanID", AvailableServiceID));

                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetAvailableServiceById", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static int DeleteAvailableService(int ServicePlanID)
        {
            //SqlParameter[] parameters =
            //{
            //    new SqlParameter("@ServicePlanID", SqlDbType.Int, 10, ParameterDirection.Input, false, 10, 0, "ServicePlanID", DataRowVersion.Proposed, ServicePlanID)
            //};
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ServicePlanID", ServicePlanID));            

            try
            {
                string ConnectionString = Common.GetConnectionString();
                int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spDeleteAvailableService", parameters.ToArray());
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
