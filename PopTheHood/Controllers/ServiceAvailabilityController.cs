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

namespace PopTheHood.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceAvailabilityController : ControllerBase
    {

        #region SaveAvailableService
        [HttpPost, Route("SaveAvailableService")]
        public IActionResult SaveAvailableService([FromBody]ServicesModel ServicesModel, string Action)
        {
            //string connectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            //List<VehicleServiceHistoryDetails> vechileList = new List<VehicleServiceHistoryDetails>();
            try
            {
                int row = Data.ServiceAvailability.SaveAvailableService(ServicesModel, Action == null ? "" : Action);

                if (row > 0)
                {
                    if (Action == "Add")
                    {
                        //return StatusCode((int)HttpStatusCode.OK, "Saved Successfully");
                        return StatusCode((int)HttpStatusCode.OK, "Saved Successfully");
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.OK, "Updated Successfully");
                    }

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { error = new { message = "Error while Saving/Updating the Service" } });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SaveAvailableService", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }
        #endregion

        #region GetAvailableService
        [HttpGet, Route("GetAvailableService")]
        public IActionResult GetAvailableService()
        {
            //string GetConnectionString = ServiceAvailabilityController.GetConnectionString();
            //string GetConnectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            List<ServicesModel> serviceList = new List<ServicesModel>();
            try
            {
                DataTable dt = Data.ServiceAvailability.GetAvailableService();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ServicesModel service = new ServicesModel();
                        service.AvailableServiceID = (int)dt.Rows[i]["AvailableServiceID"];
                        service.ServiceName = dt.Rows[i]["ServiceName"].ToString();
                        service.Description = dt.Rows[i]["Description"].ToString();
                        service.IsUserCheckApplicable = (dt.Rows[i]["IsUserCheckApplicable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsUserCheckApplicable"]);
                        service.BusinessCondition = dt.Rows[i]["BusinessCondition"].ToString();

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
                string SaveErrorLog = Data.Common.SaveErrorLog("GetAvailableService", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion

        #region GetAvailableServiceById
        [HttpGet, Route("GetAvailableServiceById/{AvailableServiceID}")]
        public IActionResult GetAvailableServiceById(int AvailableServiceID)
        {
            //string GetConnectionString = ServiceAvailabilityController.GetConnectionString();
            //string GetConnectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            List<ServicesModel> serviceList = new List<ServicesModel>();
            try
            {
                DataTable dt = Data.ServiceAvailability.GetAvailableServiceById(AvailableServiceID);

                if (dt.Rows.Count > 0)
                {
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    ServicesModel service = new ServicesModel();
                    service.AvailableServiceID = (int)dt.Rows[0]["AvailableServiceID"];
                    service.ServiceName = dt.Rows[0]["ServiceName"].ToString();
                    service.Description = dt.Rows[0]["Description"].ToString();
                    service.IsUserCheckApplicable = (dt.Rows[0]["IsUserCheckApplicable"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsUserCheckApplicable"]);
                    service.BusinessCondition = dt.Rows[0]["BusinessCondition"].ToString();

                    serviceList.Add(service);
                    //}

                    return StatusCode((int)HttpStatusCode.OK, serviceList);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { });
                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetAvailableServiceById", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion


        #region DeleteAvailableService
        [HttpDelete, Route("DeleteAvailableService/{ServicePlanID}")]
        public IActionResult DeleteAvailableService(int ServicePlanID)
        {
           //string GetConnectionString = ServiceAvailabilityController.GetConnectionString();
           //string GetConnectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
           //List<ServicesModel> serviceList = new List<ServicesModel>();
            try
            {
                int row = Data.ServiceAvailability.DeleteAvailableService(ServicePlanID);

                if (row > 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = "Deleted Successfully" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new { Error = "Invalid Service" });
                }
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("DeleteAvailableService", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }
        #endregion

        #region GetServicePlan
        [HttpGet, Route("GetServicePlan")]
        public IActionResult GetServicePlan()
        {
            List<ServicePlan> PlanList = new List<ServicePlan>();
            try
            {
                DataTable dt = Data.ServiceAvailability.GetServicePlan();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ServicePlan service = new ServicePlan();
                        service.ServicePlanID = (int)dt.Rows[i]["ServicePlanID"];
                        service.PlanType = dt.Rows[i]["PlanType"].ToString();
                        service.IsOfferPlan = (bool)dt.Rows[i]["IsOfferPlan"];

                        PlanList.Add(service);
                    }

                    return StatusCode((int)HttpStatusCode.OK, PlanList);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { });
                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetServicePlan", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion

       
    }
}