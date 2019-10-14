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
using PopTheHood.Data;
using PopTheHood.Models;

namespace PopTheHood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServicePriceChartController : ControllerBase
    {
        
        //#region DeleteServicePriceChart
        //[HttpDelete, Route("DeleteServicePriceChart/{ServicePriceChartId}")]
        //public IActionResult DeleteServicePriceChart(int ServicePriceChartId)
        //{            
        //    try
        //    {
        //        int row = Data.ServicePriceChart.DeleteServicePriceChart(ServicePriceChartId);

        //        if (row > 0)
        //        {
        //            return StatusCode((int)HttpStatusCode.OK, new { Data = "Deleted Successfully", Status = "Success" });
        //        }

        //        else
        //        {
        //            return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Error Deleting the ServicePriceChart", Status = "Error" });
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        string SaveErrorLog = Data.Common.SaveErrorLog("DeleteServicePriceChart", e.Message);

        //        return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
        //    }
        //}

        //#endregion

        #region SaveServicePriceChart
        [HttpPost, Route("SaveServicePriceChart")]
        public IActionResult SaveServicePriceChart([FromBody]ServicePriceChartModel servicePriceChart, string Action)
        {
            
            try
            {
                int row = Data.ServicePriceChart.SaveServicePriceChart(servicePriceChart, Action == null ? "" : Action);

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
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Error Saving the Service Price Chart", Status = "Error" }); 
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SaveServicePriceChart", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
            }
        }

        #endregion


        #region GetServicePriceChart
        [HttpGet, Route("GetServicePriceChart")]
        public IActionResult GetServicePriceChart()
        {
            
            //string GetConnectionString = ServicesController.GetConnectionString();
            List<ServicePriceChartModel> serviceList = new List<ServicePriceChartModel>();
            try
            {
                DataTable dt = Data.ServicePriceChart.GetServicePriceChart();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ServicePriceChartModel services = new ServicePriceChartModel();
                        services.ServicePriceChartId = (dt.Rows[i]["ServicePriceChartId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePriceChartId"]);
                        services.AvailableServiceID = (dt.Rows[i]["AvailableServiceID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["AvailableServiceID"]);
                        // services.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "-" : dt.Rows[i]["PlanType"].ToString());
                        services.ServicePlanID = (dt.Rows[i]["servicePlanID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["servicePlanID"]);
                        services.Price = (dt.Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["Price"]);
                        services.IsAvailable = (dt.Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsAvailable"]);
                        //services.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);

                        serviceList.Add(services);
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
                string SaveErrorLog = Data.Common.SaveErrorLog("GetServicePriceChart", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }
        }
        #endregion

        #region GetServicePriceChartById
        [HttpGet, Route("GetServicePriceChartById/{ServicePriceChartId}")]
        public IActionResult GetServicePriceChartById(int ServicePriceChartId)
        {
            List<ServicePriceChartModel> serviceList = new List<ServicePriceChartModel>();
            // string GetConnectionString = ServicesController.GetConnectionString();
            
            try
            {
                DataTable dt = Data.ServicePriceChart.GetServicePriceChartById(ServicePriceChartId);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ServicePriceChartModel service = new ServicePriceChartModel();
                        service.ServicePriceChartId = (dt.Rows[i]["ServicePriceChartId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePriceChartId"]);
                        service.AvailableServiceID = (dt.Rows[i]["AvailableServiceID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["AvailableServiceID"]);
                        service.ServicePlanID = (dt.Rows[i]["ServicePlanID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePlanID"]);
                        service.Price = (dt.Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["Price"]);
                        service.IsAvailable = (dt.Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsAvailable"]);
                        //service.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);

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
                string SaveErrorLog = Data.Common.SaveErrorLog("GetServicePriceChart", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }
        }
        #endregion


        #region GetServicePriceByID
        [HttpGet, Route("GetServicePriceByID/{ServicePlanID}")]
        public IActionResult GetServicePriceByID(int ServicePlanID)
        {
            List<ServicePriceChartModel> serviceList = new List<ServicePriceChartModel>();
            // string GetConnectionString = ServicesController.GetConnectionString();

            try
            {
                DataTable dt = Data.ServiceAvailability.GetServicePriceByID(ServicePlanID);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ServicePriceChartModel service = new ServicePriceChartModel();
                        service.ServicePriceChartId = (dt.Rows[i]["ServicePriceChartId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePriceChartId"]);
                        service.AvailableServiceID = (dt.Rows[i]["AvailableServiceID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["AvailableServiceID"]);
                        service.ServicePlanID = (dt.Rows[i]["ServicePlanID"] == DBNull.Value ? 0 : (int)dt.Rows[i]["ServicePlanID"]);
                        service.Price = (dt.Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["Price"]);
                        service.IsAvailable = (dt.Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsAvailable"]);
                        //service.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);

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
                string SaveErrorLog = Data.Common.SaveErrorLog("GetServicePriceChart", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }
        }
        #endregion

    }
}