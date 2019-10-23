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
using PopTheHood.Models;
using static PopTheHood.Models.ServicesModel;

namespace PopTheHood.Controllers
{
    [EnableCors("AllowAll")]
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
            List<ServiceDetails> serviceDetail = new List<ServiceDetails>();
            try
            {

                
                DataSet dt = Data.Services.SaveServiceRequest(serviceDetails);
                string row = dt.Tables[0].Rows[0]["ErrorMessage"].ToString();                
                if (row == "Success")
                {
                    //if (dt.Tables[1].Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                    //    {
                    //        ServiceDetails service = new ServiceDetails();
                    //        service.ServiceID = (dt.Tables[1].Rows[i]["ServiceID"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["ServiceID"]);
                    //        //service.ServicePlanID = (dt.Tables[1].Rows[i]["ServicePlanID"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["ServicePlanID"]);
                    //        service.ServicePriceChartId = (dt.Tables[1].Rows[i]["ServicePriceChartId"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["ServicePriceChartId"]); 
                    //        service.PlanType = (dt.Tables[1].Rows[i]["PlanType"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["PlanType"].ToString());
                    //        service.Price = (dt.Tables[1].Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Tables[1].Rows[i]["Price"]);
                    //        service.VehicleId = (dt.Tables[1].Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["VehicleId"]);
                    //        service.UserId = (dt.Tables[1].Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["UserId"]);
                    //        //service.RemainderMinutes = (dt.Tables[1].Rows[i]["RemainderMinutes"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["RemainderMinutes"]);
                    //        //service.LocationID = (dt.Tables[1].Rows[i]["LocationID"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["LocationID"]);
                    //        //service.IsTeamsandConditionsAccepted = (dt.Tables[1].Rows[i]["IsTeamsandConditionsAccepted"] == DBNull.Value ? false : (bool)dt.Tables[1].Rows[i]["IsTeamsandConditionsAccepted"]);
                    //        service.PromoCodeApplied = (dt.Tables[1].Rows[i]["PromoCodeApplied"] == DBNull.Value ? false : (bool)dt.Tables[1].Rows[i]["PromoCodeApplied"]);
                    //        service.Status = (dt.Tables[1].Rows[i]["Status"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["Status"].ToString());
                    //        service.ScheduleID = (dt.Tables[1].Rows[i]["ScheduleID"] == DBNull.Value ? 0 : (int)dt.Tables[1].Rows[i]["ScheduleID"]);
                    //        service.RequestedServiceDate = (dt.Tables[1].Rows[i]["RequestedServiceDate"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["RequestedServiceDate"].ToString());
                    //        service.ActualServiceDate = (dt.Tables[1].Rows[i]["ActualServiceDate"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["ActualServiceDate"].ToString());
                    //        service.ServiceOutDate = (dt.Tables[1].Rows[i]["ServiceOutDate"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["ServiceOutDate"].ToString());
                    //        service.ServiceName = (dt.Tables[1].Rows[i]["ServiceName"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["ServiceName"].ToString());
                    //        service.Description = (dt.Tables[1].Rows[i]["Description"] == DBNull.Value ? "-" : dt.Tables[1].Rows[i]["Description"].ToString());
                    //        service.IsAvailable = (dt.Tables[1].Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Tables[1].Rows[i]["IsAvailable"]);


                    //        serviceDetail.Add(service);
                    //    }
                    //}
                        return StatusCode((int)HttpStatusCode.OK, "Saved Successfully");
                }

                else
                    {
                        return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = row } });
                    }
                }


                catch (Exception e)
                {
                    string SaveErrorLog = Data.Common.SaveErrorLog("SaveServiceRequest", e.Message);

                    return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
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
                            //service.RemainderMinutes = (dt.Rows[i]["RemainderMinutes"] == DBNull.Value ? 0 : (int)dt.Rows[i]["RemainderMinutes"]);
                            //service.LocationID = (dt.Rows[i]["LocationID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["LocationID"]);
                            //service.IsTeamsandConditionsAccepted = (dt.Rows[i]["IsTeamsandConditionsAccepted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsTeamsandConditionsAccepted"]);
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

                        return StatusCode((int)HttpStatusCode.OK, serviceDetails);
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { });
                    }
                }

                catch (Exception e)
                {
                    string SaveErrorLog = Data.Common.SaveErrorLog("GetServiceDetailsByServiceId", e.Message);

                    return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
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
                            service.ServicePriceChartId = (dt.Rows[i]["ServicePriceChartId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePriceChartId"]); 
                            service.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "-" : dt.Rows[i]["PlanType"].ToString());
                            service.Price = (dt.Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["Price"]);
                            service.VehicleId = (dt.Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["VehicleId"]);
                            service.UserId = (dt.Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["UserId"]);
                            //service.RemainderMinutes = (dt.Rows[i]["RemainderMinutes"] == DBNull.Value ? 0 : (int)dt.Rows[i]["RemainderMinutes"]);
                            //service.LocationID = (dt.Rows[i]["LocationID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["LocationID"]);
                            //service.IsTeamsandConditionsAccepted = (dt.Rows[i]["IsTeamsandConditionsAccepted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsTeamsandConditionsAccepted"]);
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

                        return StatusCode((int)HttpStatusCode.OK, serviceDetails);
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { });
                    }
                }

                catch (Exception e)
                {
                    string SaveErrorLog = Data.Common.SaveErrorLog("UpdateServiceSchedule", e.Message);

                    return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
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

                DataSet ds = Data.Services.GetServiceDetailsLocationWise(Search);
                DataTable dt = new DataTable();
               // DataTable dt1 = new DataTable();
                dt = ds.Tables[0];
               // dt1 = ds.Tables[1];
                if (dt.Rows.Count > 0)
                {
                    var Status = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if(dt.Rows[i]["Status"].ToString() == "Service Inprogress")
                        {
                            Status = "Service on due";
                        }
                        if (dt.Rows[i]["Status"].ToString() == "Upcoming Service")
                        {
                            Status = "Next to Service";
                        }
                        else
                        {
                            Status = "No further Service";
                        }

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
                        service.Status = Status;     //dt.Rows[i]["Status"].ToString();
                        //service.TotalAmount = (dt.Rows[i]["LocationLongitude"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["LocationLongitude"]);
                        service.TotalAmount = (dt.Rows[i]["TotalAmount"] == DBNull.Value ? 00 : (decimal)dt.Rows[i]["TotalAmount"]);
                        service.Paid = (dt.Rows[i]["Amount"] == DBNull.Value ? 00 : (decimal)dt.Rows[i]["Amount"]);
                        service.Due = (dt.Rows[i]["DueAmount"] == DBNull.Value ? 00 : (decimal)dt.Rows[i]["DueAmount"]);
                       
                        serviceList.Add(service);
                    }

                    return StatusCode((int)HttpStatusCode.OK, serviceList);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { });

                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetServiceDetailsLocationWise", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }

        }
        #endregion


        #region GetServiceRequestListByVehicleId
        [HttpGet, Route("GetServiceRequestedListByVehicleId")]
        public IActionResult GetServiceRequestedList(int VehicleId)
        {
            List<ServiceDetails> serviceDetail = new List<ServiceDetails>();
            try
            {


                DataTable dt = Data.Services.GetServiceRequestListByVehicleId(VehicleId);
               // string row = dt.Tables[0].Rows[0]["ErrorMessage"].ToString();
                //if (row == "Success")
                //{
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ServiceDetails service = new ServiceDetails();
                            service.ServiceID = (dt.Rows[i]["ServiceID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServiceID"]);
                            //service.ServicePlanID = (dt.Rows[i]["ServicePlanID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePlanID"]);
                            service.ServicePriceChartId = (dt.Rows[i]["ServicePriceChartId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePriceChartId"]);
                            service.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "-" : dt.Rows[i]["PlanType"].ToString());
                            service.Price = (dt.Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["Price"]);
                            service.VehicleId = (dt.Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["VehicleId"]);
                            service.UserId = (dt.Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["UserId"]);
                            //service.RemainderMinutes = (dt.Rows[i]["RemainderMinutes"] == DBNull.Value ? 0 : (int)dt.Rows[i]["RemainderMinutes"]);
                            //service.LocationID = (dt.Rows[i]["LocationID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["LocationID"]);
                            //service.IsTeamsandConditionsAccepted = (dt.Rows[i]["IsTeamsandConditionsAccepted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsTeamsandConditionsAccepted"]);
                            service.PromoCodeApplied = (dt.Rows[i]["PromoCodeApplied"] == DBNull.Value ? false : (bool)dt.Rows[i]["PromoCodeApplied"]);
                            service.Status = (dt.Rows[i]["Status"] == DBNull.Value ? "-" : dt.Rows[i]["Status"].ToString());
                            service.ScheduleID = (dt.Rows[i]["ScheduleID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ScheduleID"]);
                            service.RequestedServiceDate = (dt.Rows[i]["RequestedServiceDate"] == DBNull.Value ? "-" : dt.Rows[i]["RequestedServiceDate"].ToString());
                            service.ActualServiceDate = (dt.Rows[i]["ActualServiceDate"] == DBNull.Value ? "-" : dt.Rows[i]["ActualServiceDate"].ToString());
                            service.ServiceOutDate = (dt.Rows[i]["ServiceOutDate"] == DBNull.Value ? "-" : dt.Rows[i]["ServiceOutDate"].ToString());
                            service.ServiceName = (dt.Rows[i]["ServiceName"] == DBNull.Value ? "-" : dt.Rows[i]["ServiceName"].ToString());
                            service.Description = (dt.Rows[i]["Description"] == DBNull.Value ? "-" : dt.Rows[i]["Description"].ToString());
                            service.IsAvailable = (dt.Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsAvailable"]);


                            serviceDetail.Add(service);
                        }

                        return StatusCode((int)HttpStatusCode.OK, serviceDetail);
                    }
                    
                //}

                    else
                    {
                        return StatusCode((int)HttpStatusCode.Forbidden, new { });
                    }
            }


            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SaveServiceRequest", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }
        #endregion


        #region SetRemainder
        [HttpPost, Route("SetRemainder")]
        public IActionResult SetRemainder(string[] ServicePriceChartId, int VehicleId, int RemainderTime)
        {
            try
            {
                if (ServicePriceChartId[0] != null)
                {
                    if(RemainderTime != 0)
                    {
                        string idString = string.Join(",", ServicePriceChartId);
                        int row = Data.Services.SetRemainder(idString, VehicleId, RemainderTime);

                        if (row > 0)
                        {
                            return StatusCode((int)HttpStatusCode.OK, "Updated Successfully");
                        }
                        else
                        {
                            return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "Error while Updating the Remainder" } });
                        }

                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = "Please Enter Remainder time" } });
                    }

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = "Please give some Service Price Chart value" } });
                }

            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SetRemainder", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion

        #region SetTeamsandConditions
        [HttpPost, Route("SetTeamsandCondition")]
        public IActionResult SetTeamsandCondition(int[] ServicePriceChartId, int VehicleId, bool Status)
        {
            try
            {
                if (ServicePriceChartId[0] != 0)
                {
                    string idString = string.Join(",", ServicePriceChartId);
                    int row = Data.Services.SetTeamsandCondition(idString, VehicleId, Status);

                    if (row > 0)
                    {
                        return StatusCode((int)HttpStatusCode.OK, "Teams and Conditions Updated Successfully");
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "Error while Updating the Teams and Conditions" } });
                    }

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = "Please give some value" } });
                }
            }
            
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SetTeamsandCondition", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion

    }
}
