using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PopTheHood.Data;
using PopTheHood.Models;

namespace PopTheHood.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
       
        #region GetUserVehicleDetails
        //GetUserVehicleDetails
        [HttpGet, Route("GetUserVehicleDetails")]
        public IActionResult GetUserVehicleDetails(int UserId, string Search)
        {
            //string GetConnectionString = VehiclesController.GetConnectionString();
            List<VehiclesDetails> vechileList = new List<VehiclesDetails>();            
            
            try
            {
               
                DataTable dt = Data.Vehicles.GetUserVehicleDetails(UserId == null ? 0 : UserId, Search == null ? "" : Search);
                if (dt.Rows.Count > 0)
                {                   
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {                           

                        if ((dt.Rows[i]["VehicleId"]) != null)
                        {
                            VehiclesDetails vechile = new VehiclesDetails();
                            //vechile.Email = (dt.Rows[i]["Email"] == DBNull.Value ? "" : dt.Rows[i]["Email"].ToString());
                            //vechile.Name = (dt.Rows[i]["Name"] == DBNull.Value ? "" : dt.Rows[i]["Name"].ToString());
                            //vechile.PhoneNumber = (dt.Rows[i]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[i]["PhoneNumber"].ToString());
                            vechile.UserId = (dt.Rows[0]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[0]["UserId"]);
                            vechile.VehicleId = (dt.Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["VehicleId"]);                        
                            vechile.Make = (dt.Rows[i]["Make"] == DBNull.Value ? "" : dt.Rows[i]["Make"].ToString());
                            vechile.Model = (dt.Rows[i]["Model"] == DBNull.Value ? "" : dt.Rows[i]["Model"].ToString());
                            vechile.Year = (dt.Rows[i]["Year"] == DBNull.Value ? 0000 : (int)dt.Rows[i]["Year"]);
                            vechile.Color = (dt.Rows[i]["Color"] == DBNull.Value ? "" : dt.Rows[i]["Color"].ToString());
                            vechile.LicensePlate = (dt.Rows[i]["LicensePlate"] == DBNull.Value ? "" : dt.Rows[i]["LicensePlate"].ToString());
                            vechile.SpecialNotes = (dt.Rows[i]["SpecialNotes"] == DBNull.Value ? "" : dt.Rows[i]["SpecialNotes"].ToString());
                            vechile.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);
                            vechile.VehicleImageURL = (dt.Rows[0]["VehicleImageURL"] == DBNull.Value ? "" : dt.Rows[0]["VehicleImageURL"].ToString());
                            //vechile.ImageType = (dt.Rows[i]["ImageType"] == DBNull.Value ? "" : dt.Rows[i]["ImageType"].ToString());

                        //vechile.UserCreatedDate = (dt.Rows[i]["UserCreatedDate"] == DBNull.Value ? "" : dt.Rows[i]["UserCreatedDate"].ToString());
                        //vechile.VehicleCreatedDate = (dt.Rows[i]["VehicleCreatedDate"] == DBNull.Value ? "" : dt.Rows[i]["VehicleCreatedDate"].ToString());
                        //vechile.IsPromoCodeApplicable = (dt.Rows[i]["IsPromoCodeApplicable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsPromoCodeApplicable"]);
                        //    vechile.IsPhoneNumVerified = (dt.Rows[i]["IsPhoneNumVerified"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsPhoneNumVerified"]);
                        //    vechile.IsPromoCodeApplicable = (dt.Rows[i]["IsPromoCodeApplicable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsPromoCodeApplicable"]);
                        //    vechile.SourceofReg = (dt.Rows[i]["SourceofReg"] == DBNull.Value ? "" : dt.Rows[i]["SourceofReg"].ToString());
                            vechileList.Add(vechile);
                        }
                    }
                  return StatusCode((int)HttpStatusCode.OK, new { Data = vechileList, Status = "Success" });
                      
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = vechileList, Status = "Success" });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetVehicleById", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }
        }
        #endregion

        #region GetVehicleById
        [HttpGet, Route("GetVehicleById/{VehicleId}")]
        public IActionResult GetVehicleById(int VehicleId)
        {
            //string GetConnectionString = VehiclesController.GetConnectionString();
            List<VehiclesDetails> vechileList = new List<VehiclesDetails>();
            try
            {
                DataTable dt = Data.Vehicles.GetVehicleById(VehicleId);

                if (dt.Rows.Count > 0)
                {
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    VehiclesDetails vechile = new VehiclesDetails();
                        vechile.VehicleId = (dt.Rows[0]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[0]["VehicleId"]);
                        vechile.UserId = (dt.Rows[0]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[0]["UserId"]);
                        vechile.Make = (dt.Rows[0]["Make"] == DBNull.Value ? "" : dt.Rows[0]["Make"].ToString());
                        vechile.Model = (dt.Rows[0]["Model"] == DBNull.Value ? "" : dt.Rows[0]["Model"].ToString());
                        vechile.Year = (dt.Rows[0]["Year"] == DBNull.Value ? 0000 : (int)dt.Rows[0]["Year"]);
                        vechile.Color = (dt.Rows[0]["Color"] == DBNull.Value ? "" : dt.Rows[0]["Color"].ToString());
                        vechile.LicensePlate = (dt.Rows[0]["LicensePlate"] == DBNull.Value ? "" : dt.Rows[0]["LicensePlate"].ToString());
                        vechile.SpecialNotes = (dt.Rows[0]["SpecialNotes"] == DBNull.Value ? "" : dt.Rows[0]["SpecialNotes"].ToString());
                        vechile.IsDeleted = (dt.Rows[0]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsDeleted"]);
                        vechile.CreatedDate = (dt.Rows[0]["CreatedDate"] == DBNull.Value ? "" : dt.Rows[0]["CreatedDate"].ToString());
                        vechile.ModifiedDate = (dt.Rows[0]["ModifiedDate"] == DBNull.Value ? "" : dt.Rows[0]["ModifiedDate"].ToString());
                    vechile.VehicleImageURL = (dt.Rows[0]["VehicleImageURL"] == DBNull.Value ? "" : dt.Rows[0]["VehicleImageURL"].ToString());
                    //vechile.ImageType = (dt.Rows[0]["ImageType"] == DBNull.Value ? "" : dt.Rows[0]["ImageType"].ToString());
                        vechileList.Add(vechile);
                    //}
                    return StatusCode((int)HttpStatusCode.OK, new { Data = vechileList, Status = "Success" });
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = vechileList, Status = "Success" });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetVehicleById", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }
        }
        #endregion

        #region DeleteVehicle
        [HttpDelete, Route("DeleteVehicle/{VehicleId}")]
        public IActionResult DeleteVehicle(int VehicleId)
        {
            //string connectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            //List<VehiclesModel> vechileList = new List<VehiclesModel>();
            try
            {
                int row = Data.Vehicles.DeleteVehicle(VehicleId);

                if (row > 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = "Deleted Successfully", Status = "Success" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Error Deleting the Vehicle", Status = "Error" });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("DeleteVehicle", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        #endregion

        #region SaveVehicle
        [HttpPost, Route("SaveVehicle")]
        public IActionResult SaveVehicle([FromBody]VehiclesModel vehiclemodel, string Action)  //int VehicleId, int UserId, string Make, string Model, int Year, string Color, string LicensePlate, string SpecialNotes
        {
            //string GetConnectionString = VehiclesController.GetConnectionString();
            
            try
            {
                if(vehiclemodel.VehicleImage.ToString() != "")
                { 
                    vehiclemodel.VehicleImageURL = AzureStorage.UploadImage(vehiclemodel.VehicleImage, Guid.NewGuid() + "." + vehiclemodel.ImageType, "vehicleimages").Result;
                }
                if (vehiclemodel.LicensePlate == "")
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Please enter LicensePlate", Status = "Error" });
                }
                else if (vehiclemodel.UserId <= 0)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Please enter UserId", Status = "Error" });
                }
                int row = Data.Vehicles.SaveVehicle(vehiclemodel, Action == null ? "" : Action);

                if (row > 0)
                {
                    if (Action == "Add")
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { Data = "Saved Successfully", Status = "Success" });
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { Data = "Updated Successfully", Status = "Success" });
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Error Saving the Service", Status = "Error" });
                }

            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SaveVehicle", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
            }
        }
        #endregion

        #region GetVehicleServiceHistoryDetails
        [HttpGet, Route("GetVehicleServiceHistoryDetails/{UserId}")]
        public IActionResult GetVehicleServiceHistoryDetails(int UserId, string Search)
        {
            //string GetConnectionString = VehiclesController.GetConnectionString();
            
            List<VehicleServiceHistoryDetails> vechileList = new List<VehicleServiceHistoryDetails>();
            try
            {
                var SearchRes = "";
                if (Search == null)
                {
                    SearchRes = "";
                }
                else
                {
                    SearchRes = Search;
                }
                DataTable dt = Data.Vehicles.GetVehicleServiceHistoryDetails(UserId, SearchRes);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        VehicleServiceHistoryDetails vechiles = new VehicleServiceHistoryDetails();
                        vechiles.VehicleId = (dt.Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["VehicleId"]);
                        vechiles.UserId = (dt.Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["UserId"]);
                        vechiles.Email = (dt.Rows[i]["Email"] == DBNull.Value ? "" : dt.Rows[i]["Email"].ToString());
                        vechiles.Name = (dt.Rows[i]["Name"] == DBNull.Value ? "" : dt.Rows[i]["Name"].ToString());
                        vechiles.PhoneNumber = (dt.Rows[i]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[i]["PhoneNumber"].ToString());
                        vechiles.Make = (dt.Rows[i]["Make"] == DBNull.Value ? "" : dt.Rows[i]["Make"].ToString());
                        vechiles.Model = (dt.Rows[i]["Model"] == DBNull.Value ? "" : dt.Rows[i]["Model"].ToString());
                        vechiles.Color = (dt.Rows[i]["Color"] == DBNull.Value ? "" : dt.Rows[i]["Color"].ToString());
                        vechiles.LicensePlate = (dt.Rows[i]["LicensePlate"] == DBNull.Value ? "" : dt.Rows[i]["LicensePlate"].ToString());
                        vechiles.ServicePlanID = (dt.Rows[i]["ServicePlanID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePlanID"]);
                        vechiles.ServiceName = (dt.Rows[i]["ServiceName"] == DBNull.Value ? "" : dt.Rows[i]["ServiceName"].ToString());
                        vechiles.ServiceDescription = (dt.Rows[i]["ServiceDescription"] == DBNull.Value ? "" : dt.Rows[i]["ServiceDescription"].ToString());
                        vechiles.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "" : dt.Rows[i]["PlanType"].ToString());
                        vechiles.ServiceAmount = (dt.Rows[i]["ServiceAmount"] == DBNull.Value ? 00 : (decimal)dt.Rows[i]["ServiceAmount"]);
                        //(dt.Rows[i]["IsEmailVerified"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsEmailVerified"]);
                        vechiles.RequestedServiceDate = (dt.Rows[i]["RequestedServiceDate"] == DBNull.Value ? "" : dt.Rows[i]["RequestedServiceDate"].ToString());
                        vechiles.ActualServiceDate = (dt.Rows[i]["ActualServiceDate"] == DBNull.Value ? "" : dt.Rows[i]["ActualServiceDate"].ToString());
                        vechiles.ServiceOutDate = (dt.Rows[i]["ServiceOutDate"] == DBNull.Value ? "" : dt.Rows[i]["ServiceOutDate"].ToString());
                        vechiles.Status = (dt.Rows[i]["Status"] == DBNull.Value ? "" : dt.Rows[i]["Status"].ToString());

                        vechiles.LocationID = (dt.Rows[i]["LocationID"] == DBNull.Value ? "" : dt.Rows[i]["LocationID"].ToString());
                        vechiles.LocationLatitude = (dt.Rows[i]["LocationLatitude"] == DBNull.Value ? "" : dt.Rows[i]["LocationLatitude"].ToString());
                        vechiles.LocationLongitude = (dt.Rows[i]["LocationLongitude"] == DBNull.Value ? "" : dt.Rows[i]["LocationLongitude"].ToString());
                        vechiles.LocationFullAddress = (dt.Rows[i]["LocationFullAddress"] == DBNull.Value ? "" : dt.Rows[i]["LocationFullAddress"].ToString());
                        vechiles.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);
                        vechiles.ServicePriceChartId = (dt.Rows[i]["ServicePriceChartId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePriceChartId"]);
                        vechiles.ScheduleID = (dt.Rows[i]["ScheduleID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ScheduleID"]);
                        vechiles.VehicleImage = ((byte[])dt.Rows[i]["VehicleImage"]);
                        vechiles.ImageType = (dt.Rows[i]["ImageType"] == DBNull.Value ? "" : dt.Rows[i]["ImageType"].ToString());

                        vechileList.Add(vechiles);
                    }
                    return StatusCode((int)HttpStatusCode.OK, new { Data = vechileList, Status = "Success" });
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = vechileList, Status = "Success" });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetVehicleServiceHistoryDetails", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }
        }
        #endregion
        

        #region SaveLocation
        [HttpPost, Route("SaveLocation")]
        public IActionResult SaveLocation([FromBody]VehicleLocation vehiclelocation, string Action) //int LocationID, int VehicleId, decimal LocationLatitude, decimal LocationLongitude, string LocationFullAddress
        {
            //List<VehicleLocation> vehicleLocation = new List<VehicleLocation>();
            //string GetConnectionString = VehiclesController.GetConnectionString();
           
            try
            {
                int row = Data.Vehicles.SaveLocation(vehiclelocation, Action == null ? "" : Action);

                if (row > 0)
                {
                    if (Action == "Add")
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { Data = "Saved Successfully", Status = "Success" });
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { Data = "Updated Successfully", Status = "Success" });
                    }
                }
                else
                {
                    //return "Invalid";
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Error while Save/Update Location", Status = "Error" });
                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SaveLocation", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
            }
        }
        #endregion

    }
}
