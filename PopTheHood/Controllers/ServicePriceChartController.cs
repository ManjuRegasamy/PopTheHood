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
        //            return StatusCode((int)HttpStatusCode.OK, new { Data = "Deleted Successfully");
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
        public IActionResult SaveServicePriceChart([FromBody]ServicePriceChartModel servicePriceChart)
        {
            string Action = "Add";
            try
            {
                int row = GetSaveServicePriceChart(servicePriceChart, Action); //Data.ServicePriceChart.SaveServicePriceChart(servicePriceChart, Action);

                if (row > 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, "Saved Successfully");
                    //}
                    //else
                    //{
                    //    return StatusCode((int)HttpStatusCode.OK, "Updated Successfully");
                    //}

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.Forbidden, new { Error = "Error Saving the Service Price Chart" }); 
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SaveServicePriceChart", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }

        #endregion

        #region UpdateServicePriceChart
        [HttpPut, Route("UpdateServicePriceChart")]
        public IActionResult UpdateServicePriceChart([FromBody]ServicePriceChartModel servicePriceChart)
        {
            string Action = "Update";
            try
            {
                int row = GetSaveServicePriceChart(servicePriceChart, Action); //Data.ServicePriceChart.SaveServicePriceChart(servicePriceChart, Action);

                if (row > 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, "Updated Successfully");                   
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.Forbidden, new { Error = "Error Updating the Service Price Chart" });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("UpdateServicePriceChart", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
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
                        services.ServiceName = (dt.Rows[i]["ServiceName"] == DBNull.Value ? "" : dt.Rows[i]["ServiceName"].ToString());
                        services.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "" : dt.Rows[i]["PlanType"].ToString());
                        services.Price = (dt.Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["Price"]);
                        services.IsAvailable = (dt.Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsAvailable"]);
                        services.Description = dt.Rows[i]["Description"].ToString();
                        services.BusinessCondition = dt.Rows[i]["BusinessCondition"].ToString();
                        services.Notes = dt.Rows[i]["Notes"].ToString();
                        //services.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);

                        serviceList.Add(services);
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
                string SaveErrorLog = Data.Common.SaveErrorLog("GetServicePriceChart", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
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
                        service.ServiceName = (dt.Rows[i]["ServiceName"] == DBNull.Value ? "" : dt.Rows[i]["ServiceName"].ToString());
                        service.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "" : dt.Rows[i]["PlanType"].ToString());
                        service.Price = (dt.Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["Price"]);
                        service.IsAvailable = (dt.Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsAvailable"]);
                        service.Description = dt.Rows[i]["Description"].ToString();
                        service.BusinessCondition = dt.Rows[i]["BusinessCondition"].ToString();
                        service.Notes = dt.Rows[i]["Notes"].ToString();
                        //service.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);

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
                string SaveErrorLog = Data.Common.SaveErrorLog("GetServicePriceChart", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
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
                        service.ServiceName = (dt.Rows[i]["ServiceName"] == DBNull.Value ? "" : dt.Rows[i]["ServiceName"].ToString());
                        service.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "" : dt.Rows[i]["PlanType"].ToString());
                        service.Price = (dt.Rows[i]["Price"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["Price"]);
                        service.IsAvailable = (dt.Rows[i]["IsAvailable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsAvailable"]);
                        service.Description = dt.Rows[i]["Description"].ToString();
                        service.BusinessCondition = dt.Rows[i]["BusinessCondition"].ToString();
                        service.Notes = dt.Rows[i]["Notes"].ToString();
                        //service.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);

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
                string SaveErrorLog = Data.Common.SaveErrorLog("GetServicePriceChart", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion



        public static int GetSaveServicePriceChart([FromBody]ServicePriceChartModel servicePriceChart, string Action)
        {
            int res = Data.ServicePriceChart.SaveServicePriceChart(servicePriceChart, Action);
            return res;
        }

    }
}