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
       
        #region Vehicles
        //GetUserVehicleDetails
        [HttpGet, Route("Vehicles")]
        public IActionResult GetUserVehicleDetails(int UserId)
        {
            //string GetConnectionString = VehiclesController.GetConnectionString();
            List<VehiclesDetails> vechileList = new List<VehiclesDetails>();            
            
            try
            {
               
                DataTable dt = Data.Vehicles.GetUserVehicleDetails(UserId == null ? 0 : UserId);
                if (dt.Rows.Count > 0)
                {                   
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {                           

                        if ((dt.Rows[i]["VehicleId"]) != null)
                        {
                            VehiclesDetails vechile = new VehiclesDetails();
                            vechile.Email = (dt.Rows[i]["Email"] == DBNull.Value ? "" : dt.Rows[i]["Email"].ToString());
                            vechile.Name = (dt.Rows[i]["Name"] == DBNull.Value ? "" : dt.Rows[i]["Name"].ToString());
                            vechile.PhoneNumber = (dt.Rows[i]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[i]["PhoneNumber"].ToString());
                            vechile.Address = (dt.Rows[i]["LocationFullAddress"] == DBNull.Value ? "" : dt.Rows[i]["LocationFullAddress"].ToString());
                            vechile.UserId = (dt.Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["UserId"]);
                            vechile.VehicleId = (dt.Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["VehicleId"]);                        
                            vechile.Make = (dt.Rows[i]["Make"] == DBNull.Value ? "" : dt.Rows[i]["Make"].ToString());
                            vechile.Model = (dt.Rows[i]["Model"] == DBNull.Value ? "" : dt.Rows[i]["Model"].ToString());
                            vechile.Year = (dt.Rows[i]["Year"] == DBNull.Value ? 0000 : (int)dt.Rows[i]["Year"]);
                            vechile.Color = (dt.Rows[i]["Color"] == DBNull.Value ? "" : dt.Rows[i]["Color"].ToString());
                            vechile.LicensePlate = (dt.Rows[i]["LicensePlate"] == DBNull.Value ? "" : dt.Rows[i]["LicensePlate"].ToString());
                            vechile.SpecialNotes = (dt.Rows[i]["SpecialNotes"] == DBNull.Value ? "" : dt.Rows[i]["SpecialNotes"].ToString());
                            vechile.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);
                            vechile.VehicleImageURL = (dt.Rows[i]["VehicleImageURL"] == DBNull.Value ? "" : dt.Rows[i]["VehicleImageURL"].ToString());
                            vechile.NextService = (dt.Rows[i]["NextService"] == DBNull.Value ? "" : dt.Rows[i]["NextService"].ToString());
                            vechile.IsServiceScheduled = (dt.Rows[i]["IsServiceScheduled"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsServiceScheduled"]);


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
                  return StatusCode((int)HttpStatusCode.OK, vechileList);
                      
                }

                else
                {
                    string[] data = new string[0];
                    return StatusCode((int)HttpStatusCode.OK, data);
                    // return StatusCode((int)HttpStatusCode.OK, new { });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("Vehicles", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion

        #region Vehicle
        [HttpGet, Route("Vehicle/{VehicleId}")]
        public IActionResult GetVehicleById(int VehicleId)
        {
            //string GetConnectionString = VehiclesController.GetConnectionString();
            List<VehiclesDetails> vechileList = new List<VehiclesDetails>();
            try
            {
                DataTable dt = Data.Vehicles.GetVehicleById(VehicleId);
                VehiclesDetails vechile = new VehiclesDetails();

                if (dt.Rows.Count > 0)
                {
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    
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
                    return StatusCode((int)HttpStatusCode.OK, vechile );
                }

                else
                {
                    string[] data = new string[0];
                    return StatusCode((int)HttpStatusCode.OK, data);
                    // return StatusCode((int)HttpStatusCode.OK, new { });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("Vehicle", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
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
                    return StatusCode((int)HttpStatusCode.OK, "Deleted Successfully");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = "Vehicle not available" } });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("DeleteVehicle", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion


        #region SaveVehicle
        [HttpPost, Route("Vehicle")]
        public IActionResult SaveVehicle([FromBody]VehiclesModel vehiclemodel)  //int VehicleId, int UserId, string Make, string Model, int Year, string Color, string LicensePlate, string SpecialNotes
        {
            //string GetConnectionString = VehiclesController.GetConnectionString();
            string Action = "Add";
            
            try
            {
                if (vehiclemodel.VehicleImage != null && vehiclemodel.VehicleImage.Length > 0)
                {
                    if (vehiclemodel.ImageType == "")
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter Image Type" } });
                    }
                    else
                    {
                        vehiclemodel.VehicleImageURL = AzureStorage.UploadImage(vehiclemodel.VehicleImage, Guid.NewGuid() + "." + vehiclemodel.ImageType, "vehicleimages").Result;
                    }
                }

                if (vehiclemodel.LicensePlate == "")
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter LicensePlate" } });
                }
                else if (vehiclemodel.UserId <= 0)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter UserId" } });
                }

              // string Results = SaveUpdateVehicle(vehiclemodel, Action);

                string Results = SaveUpdateVehicle(vehiclemodel, Action);

                if (Results != "Error Updating the Vehicle")
                {
                    int VehicleId = Convert.ToInt32(Results);
                    return StatusCode((int)HttpStatusCode.OK, new { VehicleId, message = vehiclemodel.LicensePlate + " is saved successfully"  });
                }
                else //if (Results == "Error Updating the Vehicle")
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "Vehicle not saved" } });
                }

                //if (Results == "Please enter LicensePlate")
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter LicensePlate" } });
                //}
                //else if (Results == "Please enter Image Type")
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter Image Type" } });
                //}
                //else if (Results == "Success")
                //{
                //    return StatusCode((int)HttpStatusCode.OK, "Saved Successfully");
                //}
                //else if (Results == "Please enter UserId")
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter UserId" } });
                //}
                //else //if (Results == "Error Updating the Vehicle")
                //{
                //    return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "Vehicle not saved" } });
                //}

            }            

            catch (Exception e)
            {
                if (e.Message.Contains("UNIQUE KEY constraint") == true)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "LicensePlate is already exists" } });
                }
                else
                {
                    string SaveErrorLog = Data.Common.SaveErrorLog("SaveVehicle", e.Message);

                    return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
                }
            }
        }
        #endregion

        #region UpdateVehicle
        [HttpPut, Route("Vehicle")]
        public IActionResult UpdateVehicle([FromBody]VehiclesModel vehiclemodel)
        {
            string Action = "Update";
            try
            {
              string Result = "";

                //if (vehiclemodel.ImageType == "")
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter Image Type" } });
                //}
                //else
                if (vehiclemodel.VehicleImage != null && vehiclemodel.VehicleImage.Length > 0)
                {
                    if (vehiclemodel.ImageType == "")
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter Image Type" } });
                    }
                    else
                    { 
                        vehiclemodel.VehicleImageURL = AzureStorage.UploadImage(vehiclemodel.VehicleImage, Guid.NewGuid() + "." + vehiclemodel.ImageType, "vehicleimages").Result;
                    }
                }

                if (vehiclemodel.LicensePlate == "")
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter LicensePlate" } });
                }
                else if (vehiclemodel.UserId <= 0)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter UserId" } });
                }
                                

                //if (Result == "Please enter LicensePlate")
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter LicensePlate" } });
                //}
                //else if (Result == "Please enter Image Type")
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter Image Type" } });
                //}
                //else if (Result == "Please enter UserId")
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter UserId" } });
                //}

                string Results = SaveUpdateVehicle(vehiclemodel, Action);

                if (Results != "Error Updating the Vehicle")
                {
                    int VehicleId = Convert.ToInt32(Results);
                    return StatusCode((int)HttpStatusCode.OK, new { VehicleId, message = vehiclemodel.LicensePlate + " is updated successfully" });
                }
                else //if (Results == "Error Updating the Vehicle")
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "Vehicle not saved" } });
                }

            }            
            
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("UpdateVehicle", e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
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
                        //vechiles.VehicleImage = ((byte[])dt.Rows[i]["VehicleImage"]);
                        vechiles.VehicleImageURL = (dt.Rows[0]["VehicleImageURL"] == DBNull.Value ? "" : dt.Rows[0]["VehicleImageURL"].ToString());

                       // vechiles.ImageType = (dt.Rows[i]["ImageType"] == DBNull.Value ? "" : dt.Rows[i]["ImageType"].ToString());

                        vechileList.Add(vechiles);
                    }
                    return StatusCode((int)HttpStatusCode.OK, vechileList);
                }

                else
                {
                    string[] data = new string[0];
                    return StatusCode((int)HttpStatusCode.OK, data);
                    // return StatusCode((int)HttpStatusCode.OK, new { });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetVehicleServiceHistoryDetails", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion
        

        #region SaveLocation
        [HttpPost, Route("Location")]
        public IActionResult SaveLocation([FromBody]VehicleLocation vehiclelocation) //int LocationID, int VehicleId, decimal LocationLatitude, decimal LocationLongitude, string LocationFullAddress
        {
            //List<VehicleLocation> vehicleLocation = new List<VehicleLocation>();
            //string GetConnectionString = VehiclesController.GetConnectionString();
            List<ServiceDetails> serviceDetail = new List<ServiceDetails>();
            string Action = "Add";
            try
            {
                if(vehiclelocation.VehicleId <= 0 || vehiclelocation.VehicleId == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter a VehicleId" } });
                }
                //else if (vehiclelocation.LocationLatitude == "" || vehiclelocation.LocationLatitude == "string" || vehiclelocation.LocationLatitude == null)
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter a Latitude value" } });
                //}
                //else if (vehiclelocation.LocationLongitude == "" || vehiclelocation.LocationLongitude == "string" || vehiclelocation.LocationLongitude == null)
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter a Longitude value" } });
                //}
                else if (vehiclelocation.CityName == "" || vehiclelocation.CityName == "string" || vehiclelocation.CityName == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter a CityName" } });
                }
                else
                { 
                    DataSet dt = Data.Vehicles.SaveLocation(vehiclelocation, Action);
                    string row = dt.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    if (row == "Success")
                
                    {                   
                        return StatusCode((int)HttpStatusCode.OK, "Saved Successfully");                    
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = row } });
                    }
                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SaveLocation", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }
        #endregion

        #region UpdateLocation
        [HttpPut, Route("Location")]
        public IActionResult UpdateLocation([FromBody]VehicleLocation vehiclelocation)
        {
            List<ServiceDetails> serviceDetail = new List<ServiceDetails>();

            string Action = "Update";
            try
            {
                if (vehiclelocation.LocationID <= 0 || vehiclelocation.LocationID == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter a LocationID" } });
                }
                else if (vehiclelocation.VehicleId <= 0 || vehiclelocation.VehicleId == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter a VehicleId" } });
                }
                //else if (vehiclelocation.LocationLatitude == "" || vehiclelocation.LocationLatitude == "string" || vehiclelocation.LocationLatitude == null)
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter a Latitude value" } });
                //}
                //else if (vehiclelocation.LocationLongitude == "" || vehiclelocation.LocationLongitude == "string" || vehiclelocation.LocationLongitude == null)
                //{
                //    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter a Longitude value" } });
                //}
                else if (vehiclelocation.CityName == "" || vehiclelocation.CityName == "string" || vehiclelocation.CityName == null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Please enter a CityName" } });
                }
                else
                {
                    DataSet dt = Data.Vehicles.SaveLocation(vehiclelocation, Action);
                    string row = dt.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    if (row == "Success")
                    {
                        return StatusCode((int)HttpStatusCode.OK, "Updated Successfully");
                    }
                    else
                    {
                        //return "Invalid";
                        return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = row } });
                    }
                }

                //if (row == "Success")
                //{
                //    ServiceAmount amt = new ServiceAmount();

                //    if (dt.Tables[1].Rows.Count > 0)
                //    {

                //        for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                //        {
                //            ServiceDetails service = new ServiceDetails();
                //            service.ServiceID = (dt.Tables[1].Rows[i]["ServiceID"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["ServiceID"]);
                //            //service.ServicePlanID = (dt.Tables[1].Rows[i]["ServicePlanID"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["ServicePlanID"]);
                //            service.ServicePriceChartId = (dt.Tables[1].Rows[i]["ServicePriceChartId"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["ServicePriceChartId"]);
                //            service.PlanType = (dt.Tables[1].Rows[i]["PlanType"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["PlanType"].ToString());
                //            service.Price = (dt.Tables[1].Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Tables[1].Rows[i]["Price"]);
                //            service.VehicleId = (dt.Tables[1].Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["VehicleId"]);
                //            service.UserId = (dt.Tables[1].Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["UserId"]);
                //            //service.RemainderMinutes = (dt.Tables[1].Rows[i]["RemainderMinutes"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["RemainderMinutes"]);
                //            //service.LocationID = (dt.Tables[1].Rows[i]["LocationID"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["LocationID"]);
                //            //service.IsTeamsandConditionsAccepted = (dt.Tables[1].Rows[i]["IsTeamsandConditionsAccepted"] == DBNull.Value ? false : (bool)dt.Tables[1].Rows[i]["IsTeamsandConditionsAccepted"]);
                //            service.PromoCodeApplied = (dt.Tables[1].Rows[i]["PromoCodeApplied"] == DBNull.Value ? false : (bool)dt.Tables[1].Rows[i]["PromoCodeApplied"]);
                //            service.Status = (dt.Tables[1].Rows[i]["Status"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["Status"].ToString());
                //            service.ScheduleID = (dt.Tables[1].Rows[i]["ScheduleID"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["ScheduleID"]);
                //            service.RequestedServiceDate = (dt.Tables[1].Rows[i]["RequestedServiceDate"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["RequestedServiceDate"].ToString());
                //            service.ActualServiceDate = (dt.Tables[1].Rows[i]["ActualServiceDate"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["ActualServiceDate"].ToString());
                //            service.ServiceOutDate = (dt.Tables[1].Rows[i]["ServiceOutDate"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["ServiceOutDate"].ToString());
                //            service.ServiceName = (dt.Tables[1].Rows[i]["ServiceName"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["ServiceName"].ToString());
                //            service.Description = (dt.Tables[1].Rows[i]["Description"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["Description"].ToString());
                //            service.IsAvailable = (dt.Tables[1].Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Tables[1].Rows[i]["IsAvailable"]);

                //            serviceDetail.Add(service);
                //        }

                //        if (dt.Tables[2].Rows.Count > 0)
                //        {
                //            amt.TotalAmount = (dt.Tables[2].Rows[0]["TotalAmount"] == DBNull.Value ? 0 : (decimal)dt.Tables[2].Rows[0]["TotalAmount"]);
                //        }
                //    }

                //    return StatusCode((int)HttpStatusCode.OK, new { serviceDetail, amt });

                //}

                //else
                //{
                //    return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = row } });
                //}
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("UpdateLocation", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }
        #endregion


        #region VehicleDetailedList
        [HttpGet, Route("VehicleDetailsList")]
        public IActionResult GetVehicleDetailedList(int VehicleId)
        {
            List<ServiceInfo> ServiceList = new List<ServiceInfo>();
            PaymentDetails paymentinfo = new PaymentDetails();
            PlanInfo planInfo = new PlanInfo();
            List<PlanInfo> planInfoList = new List<PlanInfo>();

            try
            {
                //var SearchRes = "";
                //if (Search == null)
                //{
                //    SearchRes = "";
                //}
                //else
                //{
                //    SearchRes = Search;
                //}
                DataSet ds = Data.Vehicles.GetVehicleDetailedList(VehicleId);
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();

                UserInfo userInfo = new UserInfo();

                VehiclesModel vehicleInfo = new VehiclesModel();
                

                // DataTable dt = Data.Vehicles.GetVehicleDetailedList(VehicleId);
                //if (ds.Tables.Count > 0)
                //{
                    dt = ds.Tables[0];
                    dt1 = ds.Tables[1];
                    dt2 = ds.Tables[2];
                    dt3 = ds.Tables[3];

                    if (dt.Rows.Count > 0)
                    {
                        userInfo.UserId = (dt.Rows[0]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[0]["UserId"]);
                        userInfo.Email = (dt.Rows[0]["Email"] == DBNull.Value ? "" : dt.Rows[0]["Email"].ToString());
                        userInfo.Name = (dt.Rows[0]["Name"] == DBNull.Value ? "" : dt.Rows[0]["Name"].ToString());
                        userInfo.PhoneNumber = (dt.Rows[0]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[0]["PhoneNumber"].ToString());
                        userInfo.IsEmailVerified = (dt.Rows[0]["IsEmailVerified"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsEmailVerified"]);
                        userInfo.IsPhoneNumVerified = (dt.Rows[0]["IsPhoneNumVerified"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsPhoneNumVerified"]);
                        userInfo.LocationID = (dt.Rows[0]["LocationID"] == DBNull.Value ? 0 : (int)dt.Rows[0]["LocationID"]);
                        userInfo.LocationLatitude = (dt.Rows[0]["LocationLatitude"] == DBNull.Value ? "-" : dt.Rows[0]["LocationLatitude"].ToString());
                        userInfo.LocationLongitude = (dt.Rows[0]["LocationLongitude"] == DBNull.Value ? "-" : dt.Rows[0]["LocationLongitude"].ToString());
                        userInfo.LocationFullAddress = (dt.Rows[0]["LocationFullAddress"] == DBNull.Value ? "" : dt.Rows[0]["LocationFullAddress"].ToString());

                        vehicleInfo.VehicleId = (dt.Rows[0]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[0]["VehicleId"]);
                        vehicleInfo.UserId = (dt.Rows[0]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[0]["UserId"]);
                        vehicleInfo.Make = (dt.Rows[0]["Make"] == DBNull.Value ? "" : dt.Rows[0]["Make"].ToString());
                        vehicleInfo.Model = (dt.Rows[0]["Model"] == DBNull.Value ? "" : dt.Rows[0]["Model"].ToString());
                        vehicleInfo.Color = (dt.Rows[0]["Color"] == DBNull.Value ? "" : dt.Rows[0]["Color"].ToString());
                        vehicleInfo.LicensePlate = (dt.Rows[0]["LicensePlate"] == DBNull.Value ? "" : dt.Rows[0]["LicensePlate"].ToString());
                        vehicleInfo.VehicleImageURL = (dt.Rows[0]["VehicleImageURL"] == DBNull.Value ? "" : dt.Rows[0]["VehicleImageURL"].ToString());
                                               
                        
                        if(dt2.Rows.Count > 0)
                        {
                           // planInfo.PlanType = (dt.Rows[0]["PlanType"] == DBNull.Value ? "" : dt.Rows[0]["PlanType"].ToString());
                            
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                PlanInfo planInfo1 = new PlanInfo();

                                planInfo1.PlanType = (dt.Rows[0]["PlanType"] == DBNull.Value ? "" : dt.Rows[0]["PlanType"].ToString());
                                planInfo1.ServiceNameList = (dt2.Rows[i]["ServiceName"] == DBNull.Value ? "" : dt2.Rows[i]["ServiceName"].ToString());
                                planInfo1.ServiceDescription = (dt2.Rows[i]["ServiceDescription"] == DBNull.Value ? "" : dt2.Rows[i]["ServiceDescription"].ToString());

                                planInfoList.Add(planInfo1);
                            }                             

                        }


                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            ServiceInfo vechiles = new ServiceInfo();
                            //vechiles.UserId = (dt.Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["UserId"]);
                            //vechiles.Email = (dt.Rows[i]["Email"] == DBNull.Value ? "" : dt.Rows[i]["Email"].ToString());
                            //vechiles.Name = (dt.Rows[i]["Name"] == DBNull.Value ? "" : dt.Rows[i]["Name"].ToString());
                            //vechiles.PhoneNumber = (dt.Rows[i]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[i]["PhoneNumber"].ToString());
                            //vechiles.LocationID = (dt.Rows[i]["LocationID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["LocationID"]);
                            //vechiles.LocationLatitude = (dt.Rows[i]["LocationLatitude"] == DBNull.Value ? "" : dt.Rows[i]["LocationLatitude"].ToString());
                            //vechiles.LocationLongitude = (dt.Rows[i]["LocationLongitude"] == DBNull.Value ? "" : dt.Rows[i]["LocationLongitude"].ToString());
                            //vechiles.LocationFullAddress = (dt.Rows[i]["LocationFullAddress"] == DBNull.Value ? "" : dt.Rows[i]["LocationFullAddress"].ToString());
                            //vechiles.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);

                            //vechiles.VehicleId = (dt.Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["VehicleId"]);
                            //vechiles.Make = (dt.Rows[i]["Make"] == DBNull.Value ? "" : dt.Rows[i]["Make"].ToString());
                            //vechiles.Model = (dt.Rows[i]["Model"] == DBNull.Value ? "" : dt.Rows[i]["Model"].ToString());
                            //vechiles.Color = (dt.Rows[i]["Color"] == DBNull.Value ? "" : dt.Rows[i]["Color"].ToString());
                            //vechiles.LicensePlate = (dt.Rows[i]["LicensePlate"] == DBNull.Value ? "" : dt.Rows[i]["LicensePlate"].ToString());
                            //vechiles.VehicleImage = ((byte[])dt.Rows[i]["VehicleImage"]);
                            //vechiles.VehicleImageURL = (dt.Rows[0]["VehicleImageURL"] == DBNull.Value ? "" : dt.Rows[0]["VehicleImageURL"].ToString());

                            //vechiles.ServiceDescription = (dt.Rows[i]["ServiceDescription"] == DBNull.Value ? "" : dt.Rows[i]["ServiceDescription"].ToString());
                            //vechiles.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "" : dt.Rows[i]["PlanType"].ToString());
                            vechiles.ServicePlanID = (dt.Rows[i]["ServicePlanID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePlanID"]);
                            vechiles.ServiceID = (dt.Rows[i]["ServiceID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServiceID"]);
                            vechiles.ServiceName = (dt.Rows[1]["ServiceNameList"] == DBNull.Value ? "" : dt.Rows[1]["ServiceNameList"].ToString());
                            vechiles.ServiceAmount = (dt3.Rows[i]["Amount"] == DBNull.Value ? 00 : (decimal)dt3.Rows[i]["Amount"]);
                            //(dt.Rows[i]["IsEmailVerified"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsEmailVerified"]);
                            vechiles.RequestedServiceDate = (dt3.Rows[i]["RequestedServiceDate"] == DBNull.Value ? "" : dt3.Rows[i]["RequestedServiceDate"].ToString());
                            vechiles.RemainderMinutes = (dt.Rows[i]["RemainderMinutes"] == DBNull.Value ? 0 : (int)dt.Rows[i]["RemainderMinutes"]);
                            vechiles.ActualServiceDate = (dt.Rows[i]["ActualServiceDate"] == DBNull.Value ? "" : dt.Rows[i]["ActualServiceDate"].ToString());
                            vechiles.ServiceOutDate = (dt3.Rows[i]["ServiceOutDate"] == DBNull.Value ? "" : dt3.Rows[i]["ServiceOutDate"].ToString());
                            vechiles.Status = (dt3.Rows[i]["Status"] == DBNull.Value ? "" : dt3.Rows[i]["Status"].ToString());

                            vechiles.IsTeamsandConditionsAccepted = (dt.Rows[i]["IsTeamsandConditionsAccepted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsTeamsandConditionsAccepted"]);
                            vechiles.ServicePriceChartId = (dt.Rows[i]["ServicePriceChartId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePriceChartId"]);
                            vechiles.ScheduleID = (dt3.Rows[i]["ScheduleID"] == DBNull.Value ? 0 : (int)dt3.Rows[i]["ScheduleID"]);

                            vechiles.Comments = (dt3.Rows[i]["Comments"] == DBNull.Value ? "-" : dt3.Rows[i]["Comments"].ToString());


                            //vechiles.PaymentDetailId = (dt.Rows[i]["PaymentDetailId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["PaymentDetailId"]);
                            //vechiles.PaymentType = (dt.Rows[i]["PaymentType"] == DBNull.Value ? "" : dt.Rows[i]["PaymentType"].ToString());
                            //vechiles.Amount = (dt.Rows[i]["Amount"] == DBNull.Value ? 00 : (decimal)dt.Rows[i]["Amount"]);
                            //vechiles.Promocode_ReducedAmount = (dt.Rows[i]["Promocode_ReducedAmount"] == DBNull.Value ? 00 : (decimal)dt.Rows[i]["Promocode_ReducedAmount"]);
                            //vechiles.PaymentDate = (dt.Rows[i]["PaymentDate"] == DBNull.Value ? "" : dt.Rows[i]["PaymentDate"].ToString());
                            //vechiles.PaymentStatus = (dt.Rows[i]["PaymentStatus"] == DBNull.Value ? "" : dt.Rows[i]["PaymentStatus"].ToString());

                            ServiceList.Add(vechiles);

                        }

                        
                        if (dt1.Rows.Count > 0)
                        {
                            paymentinfo.PaymentDetailId = (dt.Rows[0]["PaymentDetailId"] == DBNull.Value ? 0 : (int)dt.Rows[0]["PaymentDetailId"]);
                            paymentinfo.PaymentType = (dt.Rows[0]["PaymentType"] == DBNull.Value ? "" : dt.Rows[0]["PaymentType"].ToString());
                            paymentinfo.TotalAmount = (dt1.Rows[0]["TotalAmount"] == DBNull.Value ? 00 : (decimal)dt1.Rows[0]["TotalAmount"]);
                            paymentinfo.Paid = (dt.Rows[0]["Amount"] == DBNull.Value ? 00 : (decimal)dt.Rows[0]["Amount"]);
                            paymentinfo.Due = ((dt1.Rows[0]["TotalAmount"] == DBNull.Value ? 00 : (decimal)dt1.Rows[0]["TotalAmount"]) - (dt.Rows[0]["Amount"] == DBNull.Value ? 00 : (decimal)dt.Rows[0]["Amount"]));
                            //(dt1.Rows[0]["Amount"] == DBNull.Value ? 00 : (decimal)dt1.Rows[0]["Amount"]);
                            paymentinfo.Promocode_ReducedAmount = (dt.Rows[0]["Promocode_ReducedAmount"] == DBNull.Value ? "" : dt.Rows[0]["Promocode_ReducedAmount"].ToString());
                            paymentinfo.PaymentDate = (dt.Rows[0]["PaymentDate"] == DBNull.Value ? "" : dt.Rows[0]["PaymentDate"].ToString());
                            paymentinfo.PaymentStatus = (dt.Rows[0]["PaymentStatus"] == DBNull.Value ? "" : dt.Rows[0]["PaymentStatus"].ToString());
                        }

                        return StatusCode((int)HttpStatusCode.OK, new { userInfo, vehicleInfo, planInfoList, ServiceList, paymentinfo });
                    }

                    else
                    {
                        string[] data = new string[0];
                        return StatusCode((int)HttpStatusCode.OK, data);
                        // return StatusCode((int)HttpStatusCode.OK, new { });
                    }
                //}
                
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("VehicleDetailsList", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }
        #endregion

        public static string SaveUpdateVehicle([FromBody]VehiclesModel vehiclemodel, string Action)
        {
            string Result = "";
            try
            {
                //if(vehiclemodel.ImageType == "")
                //{
                //    Result = "Please enter Image Type";
                //}
                //else if (vehiclemodel.VehicleImage.ToString() != "")
                //{
                //    vehiclemodel.VehicleImageURL = AzureStorage.UploadImage(vehiclemodel.VehicleImage, Guid.NewGuid() + "." + vehiclemodel.ImageType, "vehicleimages").Result;
                //}
                
                //if (vehiclemodel.LicensePlate == "")
                //{
                //    Result = "Please enter License Plate" ;
                //}
                //else if (vehiclemodel.UserId <= 0)
                //{
                //    Result = "Please enter UserId";
                //}
                //else
                //{
                    DataTable dt = Data.Vehicles.SaveVehicle(vehiclemodel, Action);
                    //VehiclesDetails vechile = new VehiclesDetails();

                    if (dt.Rows[0]["VehicleId"].ToString() != "")
                    {
                        Result = (dt.Rows[0]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[0]["VehicleId"]).ToString();
                    }
                    else
                    {
                        Result = "Error Updating the Vehicle";
                    }




                //}

                return Result;

            }

            catch (Exception e)
            {
                throw e;               
            }
        }

    }
}
