using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PopTheHood.Models;
using static PopTheHood.Models.ServicesModel;

namespace PopTheHood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServicesController : ControllerBase
    {
        

        #region SaveServiceRequest
        [HttpPost, Route("SaveServiceRequest")]
            public IActionResult SaveServiceRequest([FromBody]ServiceRequest serviceDetails)
            {
                //string connectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            try
            {
                    string row = Data.Services.SaveServiceRequest(serviceDetails);

                    if (row == "Success")
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { Data = "Saved Successfully", Status = "Success" });
                    }

                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Error while Saving the ServiceRequest", Status = "Error" });
                    }
                }


                catch (Exception e)
                {
                    string SaveErrorLog = Data.Common.SaveErrorLog("SaveServiceRequest", e.Message);

                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
                }
            }
            #endregion

        #region GetServiceDetailsByServiceId
            [HttpGet, Route("GetServiceDetailsByServiceId/{ServiceID}")]
            public IActionResult GetServiceDetailsByServiceId(int ServiceID)
            {
            //string GetConnectionString = ServicesController.GetConnectionString();
            //string GetConnectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            List<ServiceDetails> serviceDetails = new List<ServiceDetails>();
                try
                {
                    DataTable dt = Data.Services.GetServiceDetailsByServiceId(ServiceID);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ServiceDetails service = new ServiceDetails();
                            service.ServiceID = (dt.Rows[i]["ServiceID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServiceID"]);
                           // service.ServicePlanID = (dt.Rows[i]["ServicePlanID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePlanID"]);
                            service.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "-" : dt.Rows[i]["PlanType"].ToString());
                            service.Price = (dt.Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["Price"]);
                            service.VehicleId = (dt.Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["VehicleId"]);
                            service.UserId = (dt.Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["UserId"]);
                            service.RemainderMinutes = (dt.Rows[i]["RemainderMinutes"] == DBNull.Value ? 0 : (int)dt.Rows[i]["RemainderMinutes"]);
                            service.LocationID = (dt.Rows[i]["LocationID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["LocationID"]);
                            service.IsTeamsandConditionsAccepted = (dt.Rows[i]["IsTeamsandConditionsAccepted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsTeamsandConditionsAccepted"]);
                            service.PromoCodeApplied = (dt.Rows[i]["PromoCodeApplied"] == DBNull.Value ? false : (bool)dt.Rows[i]["PromoCodeApplied"]);
                            service.Status = (dt.Rows[i]["Status"] == DBNull.Value ? "-" : dt.Rows[i]["Status"].ToString());
                            service.ScheduleID = (dt.Rows[i]["ScheduleID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ScheduleID"]);
                            service.RequestedServiceDate = (dt.Rows[i]["RequestedServiceDate"] == DBNull.Value ? "-" : dt.Rows[i]["RequestedServiceDate"].ToString());
                            service.ActualServiceDate = (dt.Rows[i]["ActualServiceDate"] == DBNull.Value ? "-" : dt.Rows[i]["ActualServiceDate"].ToString());
                            service.ServiceOutDate = (dt.Rows[i]["ServiceOutDate"] == DBNull.Value ? "-" : dt.Rows[i]["ServiceOutDate"].ToString());
                            service.ServiceName = (dt.Rows[i]["ServiceName"] == DBNull.Value ? "-" : dt.Rows[i]["ServiceName"].ToString());
                            service.Description = (dt.Rows[i]["Description"] == DBNull.Value ? "-" : dt.Rows[i]["Description"].ToString());
                            service.IsAvailable = (dt.Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsAvailable"]);

                            serviceDetails.Add(service);
                        }

                        return StatusCode((int)HttpStatusCode.OK, new { Data = serviceDetails, Status = "Success" });
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { Data = serviceDetails, Status = "Success" });
                    }
                }

                catch (Exception e)
                {
                    string SaveErrorLog = Data.Common.SaveErrorLog("GetServiceDetailsByServiceId", e.Message);

                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
                }
            }
            #endregion

        #region UpdateServiceSchedule
            [HttpPut, Route("UpdateServiceSchedule")]
            public IActionResult UpdateServiceSchedule([FromBody]ServiceSchedule servicedetails)
            {
            //string GetConnectionString = ServicesController.GetConnectionString();
            //string GetConnectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            List<ServiceDetails> serviceDetails = new List<ServiceDetails>();
                try
                {                   
                    DataTable dt = Data.Services.GetUpdateServiceSchedule(servicedetails);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ServiceDetails service = new ServiceDetails();
                            service.ServiceID = (dt.Rows[i]["ServiceID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServiceID"]);
                            //service.ServicePlanID = (dt.Rows[i]["ServicePlanID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePlanID"]);
                            service.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "-" : dt.Rows[i]["PlanType"].ToString());
                            service.Price = (dt.Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["Price"]);
                            service.VehicleId = (dt.Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["VehicleId"]);
                            service.UserId = (dt.Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["UserId"]);
                            service.RemainderMinutes = (dt.Rows[i]["RemainderMinutes"] == DBNull.Value ? 0 : (int)dt.Rows[i]["RemainderMinutes"]);
                            service.LocationID = (dt.Rows[i]["LocationID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["LocationID"]);
                            service.IsTeamsandConditionsAccepted = (dt.Rows[i]["IsTeamsandConditionsAccepted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsTeamsandConditionsAccepted"]);
                            service.PromoCodeApplied = (dt.Rows[i]["PromoCodeApplied"] == DBNull.Value ? false : (bool)dt.Rows[i]["PromoCodeApplied"]);
                            service.Status = (dt.Rows[i]["Status"] == DBNull.Value ? "-" : dt.Rows[i]["Status"].ToString());
                            service.ScheduleID = (dt.Rows[i]["ScheduleID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ScheduleID"]);
                            service.RequestedServiceDate = (dt.Rows[i]["RequestedServiceDate"] == DBNull.Value ? "-" : dt.Rows[i]["RequestedServiceDate"].ToString());
                            service.ActualServiceDate = (dt.Rows[i]["ActualServiceDate"] == DBNull.Value ? "-" : dt.Rows[i]["ActualServiceDate"].ToString());
                            service.ServiceOutDate = (dt.Rows[i]["ServiceOutDate"] == DBNull.Value ? "-" : dt.Rows[i]["ServiceOutDate"].ToString());
                            service.ServiceName = (dt.Rows[i]["ServiceName"] == DBNull.Value ? "-" : dt.Rows[i]["ServiceName"].ToString());
                            service.Description = (dt.Rows[i]["Description"] == DBNull.Value ? "-" : dt.Rows[i]["Description"].ToString());
                            service.IsAvailable = (dt.Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsAvailable"]);

                            serviceDetails.Add(service);
                        }

                        return StatusCode((int)HttpStatusCode.OK, new { Data = serviceDetails, Status = "Success" });
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { Data = serviceDetails, Status = "Success" });
                    }
                }

                catch (Exception e)
                {
                    string SaveErrorLog = Data.Common.SaveErrorLog("UpdateServiceSchedule", e.Message);

                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
                }
            }

            #endregion

           
        #region GetServiceDetailsLocationWise
        [HttpGet, Route("GetServiceDetailsLocationWise")]
        public IActionResult GetServiceDetailsLocationWise(string Search)
        {
            //string GetConnectionString = ServicesController.GetConnectionString();
            //string GetConnectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            List<ServiceLocation> serviceList = new List<ServiceLocation>();
            try
            {
                if (Search == null)
                {
                    Search = "";
                }

                DataTable dt = Data.Services.GetServiceDetailsLocationWise(Search);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ServiceLocation service = new ServiceLocation();
                        service.UserId = (dt.Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["UserId"]);
                        service.Name = (dt.Rows[i]["Name"] == DBNull.Value ? "-" : dt.Rows[i]["Name"].ToString());
                        service.Email = (dt.Rows[i]["Email"] == DBNull.Value ? "-" : dt.Rows[i]["Email"].ToString());
                        service.PhoneNumber = (dt.Rows[i]["PhoneNumber"] == DBNull.Value ? "-" : dt.Rows[i]["PhoneNumber"].ToString());
                        service.VehicleId = (dt.Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["VehicleId"]);
                        service.Make = (dt.Rows[i]["Make"] == DBNull.Value ? "-" : dt.Rows[i]["Make"].ToString());
                        service.Model = (dt.Rows[i]["Model"] == DBNull.Value ? "-" : dt.Rows[i]["Model"].ToString());
                        service.Color = (dt.Rows[i]["Color"] == DBNull.Value ? "-" : dt.Rows[i]["Color"].ToString());
                        service.LicensePlate = (dt.Rows[i]["LicensePlate"] == DBNull.Value ? "-" : dt.Rows[i]["LicensePlate"].ToString());
                       // service.ServicePlanID = (dt.Rows[i]["ServicePlanID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePlanID"]);
                        service.ServiceName = (dt.Rows[i]["ServiceName"] == DBNull.Value ? "-" : dt.Rows[i]["ServiceName"].ToString());
                        service.ServiceDescription = (dt.Rows[i]["ServiceDescription"] == DBNull.Value ? "-" : dt.Rows[i]["ServiceDescription"].ToString());
                        service.LocationLatitude = (dt.Rows[i]["LocationLatitude"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["LocationLatitude"]);
                        service.LocationLongitude = (dt.Rows[i]["LocationLongitude"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["LocationLongitude"]);
                        service.LocationFullAddress = (dt.Rows[i]["LocationFullAddress"] == DBNull.Value ? "-" : dt.Rows[i]["LocationFullAddress"].ToString());
                        service.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "-" : dt.Rows[i]["PlanType"].ToString());
                        service.ServiceAmount = (dt.Rows[i]["ServiceAmount"] == DBNull.Value ? 00 : (decimal)dt.Rows[i]["ServiceAmount"]);
                        service.RequestedServiceDate = (dt.Rows[i]["RequestedServiceDate"] == DBNull.Value ? "-" : dt.Rows[i]["RequestedServiceDate"].ToString());
                        service.ActualServiceDate = (dt.Rows[i]["ActualServiceDate"] == DBNull.Value ? "-" : dt.Rows[i]["ActualServiceDate"].ToString());
                        service.ServiceOutDate = (dt.Rows[i]["ServiceOutDate"] == DBNull.Value ? "-" : dt.Rows[i]["ServiceOutDate"].ToString());
                        service.Status = dt.Rows[i]["Status"].ToString();

                        serviceList.Add(service);
                    }

                    return StatusCode((int)HttpStatusCode.OK, new { Data = serviceList, Status = "Success" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = serviceList, Status = "Success" });

                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetServiceDetailsLocationWise", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }

        }
        #endregion


    }
}
