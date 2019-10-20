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
    public class Vehicles
    {
        public static DataTable GetVehicleById(int VehicleId)
        {
       
            try
            {
                string ConnectionString = Common.GetConnectionString();
                //Create the parameters in the SqlParameter array
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@VehicleId", VehicleId));


                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetVehicleById", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        public static DataTable GetUserVehicleDetails(int UserId)
        {

            try
            {
                string ConnectionString = Common.GetConnectionString();
                //Create the parameters in the SqlParameter array
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", UserId));

                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetUserVehicleDetails", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static int DeleteVehicle(int VehicleId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@VehicleId", VehicleId));

            try
            {
                string ConnectionString = Common.GetConnectionString();
                int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spDeleteVehicle", parameters.ToArray());
                return rowsAffected;
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }
        }

        public static int SaveVehicle(VehiclesModel vehiclemodel, string Action)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", vehiclemodel.UserId));
                parameters.Add(new SqlParameter("@VehicleId", vehiclemodel.VehicleId));
                parameters.Add(new SqlParameter("@Make", vehiclemodel.Make));
                parameters.Add(new SqlParameter("@Model", vehiclemodel.Model));
                parameters.Add(new SqlParameter("@Year", vehiclemodel.Year));
                parameters.Add(new SqlParameter("@Color", vehiclemodel.Color));
                parameters.Add(new SqlParameter("@LicensePlate", vehiclemodel.LicensePlate));
                parameters.Add(new SqlParameter("@SpecialNotes", vehiclemodel.SpecialNotes));
                parameters.Add(new SqlParameter("@VehicleImageURL", vehiclemodel.VehicleImageURL));
                parameters.Add(new SqlParameter("@ImageType", vehiclemodel.ImageType));
                parameters.Add(new SqlParameter("@Action", Action));

                //using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spSaveVehicle", parameters.ToArray()).Tables[0])
                //{
                    int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spSaveVehicle", parameters.ToArray());
                    return rowsAffected;
                //}
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }
        }

        public static DataTable GetVehicleServiceHistoryDetails(int UserId, string Search)
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
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetVehicleServiceHistoryDetails", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        

        public static int SaveLocation(VehicleLocation vehiclelocation, string Action)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@LocationID", vehiclelocation.LocationID));
                parameters.Add(new SqlParameter("@VehicleId", vehiclelocation.VehicleId));
                parameters.Add(new SqlParameter("@LocationLatitude", vehiclelocation.LocationLatitude));
                parameters.Add(new SqlParameter("@LocationLongitude", vehiclelocation.LocationLongitude));
                parameters.Add(new SqlParameter("@LocationFullAddress", vehiclelocation.LocationFullAddress));
                parameters.Add(new SqlParameter("@LandMark", vehiclelocation.LandMark)); 
                parameters.Add(new SqlParameter("@Action", Action));

                int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spSaveLocation", parameters.ToArray());
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
