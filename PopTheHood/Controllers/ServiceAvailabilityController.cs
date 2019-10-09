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

namespace PopTheHood.Controllers
{
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
                        return StatusCode((int)HttpStatusCode.OK, new { Data = "Saved Successfully", Status = "Success" });
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { Data = "Updated Successfully", Status = "Success" });
                    }

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Error while Saving/Updating the Service", Status = "Error" });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SaveAvailableService", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
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
                string SaveErrorLog = Data.Common.SaveErrorLog("GetAvailableService", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
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

                    serviceList.Add(service);
                    //}

                    return StatusCode((int)HttpStatusCode.OK, new { Data = serviceList, Status = "Success" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = serviceList, Status = "Success" });
                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetAvailableServiceById", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
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
                    return StatusCode((int)HttpStatusCode.OK, new { Data = "Deleted Successfully", Status = "Success" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Invalid UserName", Status = "Error" });
                }
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("DeleteAvailableService", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
            }
        }
        #endregion
    }
}