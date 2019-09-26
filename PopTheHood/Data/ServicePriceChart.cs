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
    public class ServicePriceChart
    {

        public static DataTable GetServicePriceChart()
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetServicePriceChart").Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataTable GetServicePriceChartById(int ServicePriceChartId)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                //SqlParameter[] parameters =
                //{
                //    new SqlParameter("@ServicePriceChartId", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 10, 0, "ServicePriceChartId", DataRowVersion.Proposed, ServicePriceChartId)
                //};
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ServicePriceChartId", ServicePriceChartId));
               
                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetServicePriceChartById", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int SaveServicePriceChart(ServicePriceChartModel servicePriceChart, string Action)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@ServicePriceChartId", servicePriceChart.ServicePriceChartId));
                parameters.Add(new SqlParameter("@ServicePlanID", servicePriceChart.ServicePlanID));
                parameters.Add(new SqlParameter("@PlanType", servicePriceChart.PlanType));
                parameters.Add(new SqlParameter("@IsAvailable", servicePriceChart.IsAvailable));
                parameters.Add(new SqlParameter("@Price", servicePriceChart.Price));
                parameters.Add(new SqlParameter("@Action", Action));

                int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spServicePriceChart", parameters.ToArray());
                return rowsAffected;
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }
        }

        public static int DeleteServicePriceChart(int ServicePriceChartId)
        {
            // SqlParameter[] parameters =
            //{
            //     new SqlParameter("@ServicePriceChartId", SqlDbType.Int, 10, ParameterDirection.Input, false, 10, 0, "ServicePriceChartId", DataRowVersion.Proposed, ServicePriceChartId)
            // };
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ServicePriceChartId", ServicePriceChartId));
           
            try
            {
                string ConnectionString = Common.GetConnectionString();

                int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spDeleteServicePriceChart", parameters.ToArray());
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
