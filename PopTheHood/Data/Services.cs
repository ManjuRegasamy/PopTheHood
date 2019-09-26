using Microsoft.ApplicationBlocks.Data;
using PopTheHood.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PopTheHood.Data
{
    public class Services
    {

        public static DataTable GetServiceDetailsByServiceId(int ServiceID)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ServiceID", ServiceID));

                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "SpGetServiceDetailsByServiceId", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataTable GetUpdateServiceSchedule(ServiceSchedule servicedetails)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ScheduleID", servicedetails.ScheduleID));
                parameters.Add(new SqlParameter("@ServiceID", servicedetails.ServiceID));
                parameters.Add(new SqlParameter("@RequestedServiceDate", Convert.ToDateTime(servicedetails.RequestedServiceDate)));
                parameters.Add(new SqlParameter("@ActualServiceDate", Convert.ToDateTime(servicedetails.ActualServiceDate)));
                parameters.Add(new SqlParameter("@ServiceOutDate", Convert.ToDateTime(servicedetails.ServiceOutDate)));
                parameters.Add(new SqlParameter("@Status", servicedetails.Status));

                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spUpdateServiceSchedule", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       
        public static DataTable GetServiceDetailsLocationWise(string Search)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                //Create the parameters in the SqlParameter array
                //    SqlParameter[] parameters =
                //{
                //    new SqlParameter("@Search", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 10, 0, "Search", DataRowVersion.Proposed, Search)
                //};
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Search", Search));

                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetServiceDetailsLocationWise", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static string SaveServiceRequest(ServiceRequest serviceDetails)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ServicePlanID", serviceDetails.ServicePlanID));
                parameters.Add(new SqlParameter("@ServicePriceChartId", serviceDetails.ServicePriceChartId));
                parameters.Add(new SqlParameter("@VehicleId", serviceDetails.VehicleId));
                parameters.Add(new SqlParameter("@RemainderMinutes", serviceDetails.RemainderMinutes));
                parameters.Add(new SqlParameter("@LocationID", serviceDetails.LocationID));
                parameters.Add(new SqlParameter("@IsTeamsandConditionsAccepted", serviceDetails.IsTeamsandConditionsAccepted));
                parameters.Add(new SqlParameter("@PromoCodeApplied", serviceDetails.PromoCodeApplied));
                parameters.Add(new SqlParameter("@PlanType", serviceDetails.PlanType));


                //int rowsAffected = SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "spSaveUser", parameters.ToArray());
                //    SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "spSaveUser", parameters.ToArray());
                //return rowsAffected.ToString();
                using (DataSet dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spSaveServiceRequest", parameters.ToArray()))
                {
                    string rowsAffected = dt.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    return rowsAffected;
                }
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }

        }

        

    }
}
