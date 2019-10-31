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
                var ServiceOutDate = "1900-01-01";
                if (servicedetails.ServiceOutDate!="" && servicedetails.ServiceOutDate != null && servicedetails.ServiceOutDate != "string")
                {
                    ServiceOutDate = servicedetails.ServiceOutDate;
                }
                //else
                //{
                //    ServiceOutDate = Convert.ToDateTime("1900-01-01");
                //}
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ScheduleID", servicedetails.ScheduleID));
               // parameters.Add(new SqlParameter("@ServiceID", servicedetails.ServiceID));
                parameters.Add(new SqlParameter("@RequestedServiceDate", Convert.ToDateTime(servicedetails.RequestedServiceDate)));
                //parameters.Add(new SqlParameter("@ActualServiceDate", Convert.ToDateTime(servicedetails.ActualServiceDate)));
                parameters.Add(new SqlParameter("@ServiceOutDate", Convert.ToDateTime(ServiceOutDate)));
                parameters.Add(new SqlParameter("@Status", servicedetails.Status));
                parameters.Add(new SqlParameter("@Comments", servicedetails.Comments));

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
       
        public static DataSet GetServiceDetailsLocationWise(string Search)
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
                using (DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetServiceDetailsLocationWise", parameters.ToArray()))
                {
                    return ds;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet SaveServiceRequest(ServiceRequest serviceDetails)
        {
            try
            {
                int[] ServicePriceChartId = serviceDetails.ServicePriceChartId;          // { "2343", "2344", "2345" };
                string idString = string.Join(",", ServicePriceChartId);

                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ServicePlanID", serviceDetails.ServicePlanID));
                parameters.Add(new SqlParameter("@ServicePriceChartId", idString));
                parameters.Add(new SqlParameter("@VehicleId", serviceDetails.VehicleId));
                //parameters.Add(new SqlParameter("@RemainderMinutes", serviceDetails.RemainderMinutes));
                //parameters.Add(new SqlParameter("@LocationID", serviceDetails.LocationID));
                //parameters.Add(new SqlParameter("@IsTeamsandConditionsAccepted", serviceDetails.IsTeamsandConditionsAccepted));
                parameters.Add(new SqlParameter("@PromoCodeApplied", serviceDetails.PromoCodeApplied));
                


                //int rowsAffected = SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "spSaveUser", parameters.ToArray());
                //    SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "spSaveUser", parameters.ToArray());
                //return rowsAffected.ToString();
                using (DataSet dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spSaveServiceRequest", parameters.ToArray()))
                {
                    return dt;
                    //DataTable rowsAffected = dt.Tables[0];
                    //if(rowsAffected.ToString() == "Success")
                    //{
                    //    return dt.Tables[1];
                    //}
                    //else
                    //{ 
                    //    return dt.Tables[0];
                    //}
                }
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }

        }

        public static DataTable GetServiceRequestListByVehicleId (int VehicleId)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@VehicleId", VehicleId));

                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetServiceRequestListByVehicleId", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int SetRemainder(int ServiceId, int RemainderTime)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ServiceId", ServiceId));
            parameters.Add(new SqlParameter("@RemainderTime", RemainderTime));

            try
            {
                string ConnectionString = Common.GetConnectionString();

                int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spSetRemainder", parameters.ToArray());
                return rowsAffected;
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }
        }
        
        public static int SetTeamsandCondition( int ServiceId, bool Status)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ServiceId", ServiceId));
            parameters.Add(new SqlParameter("@Status", Status));

            try
            {
                string ConnectionString = Common.GetConnectionString();

                int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spSetTeamsandConditions", parameters.ToArray());
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
