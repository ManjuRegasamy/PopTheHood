using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using PopTheHood.Models;
using static PopTheHood.Models.PaymentModel;
using Microsoft.AspNetCore.Cors;

namespace PopTheHood.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
       
        #region GetVehiclePaymentDetails
        [HttpGet, Route("GetVehiclePaymentDetails/{UserId}")]
        public IActionResult GetVehiclePaymentDetails(int UserId, string Search)
        {
            //string GetConnectionString = PaymentController.GetConnectionString();
            //string GetConnectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            List<VehiclePaymentDetails> vechileList = new List<VehiclePaymentDetails>();
            try
            {                
                DataTable dt = Data.Payment.GetVehiclePaymentDetails(UserId == null ? 0 : UserId, Search == null ? "" : Search);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        VehiclePaymentDetails vechile = new VehiclePaymentDetails();
                        vechile.VehicleId = (dt.Rows[i]["VehicleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["VehicleId"]); 
                        vechile.UserId = (dt.Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["UserId"]);
                        vechile.Email = (dt.Rows[i]["Email"] == DBNull.Value ? "" : dt.Rows[i]["Email"].ToString());
                        vechile.Name = (dt.Rows[i]["Name"] == DBNull.Value ? "" : dt.Rows[i]["Name"].ToString());
                        vechile.PhoneNumber = (dt.Rows[i]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[i]["PhoneNumber"].ToString());
                        vechile.Make = (dt.Rows[i]["Make"] == DBNull.Value ? "" : dt.Rows[i]["Make"].ToString());
                        vechile.Model = (dt.Rows[i]["Model"] == DBNull.Value ? "" : dt.Rows[i]["Model"].ToString());
                        vechile.Color = (dt.Rows[i]["Color"] == DBNull.Value ? "" : dt.Rows[i]["Color"].ToString());
                        vechile.LicencePlate = (dt.Rows[i]["LicensePlate"] == DBNull.Value ? "" : dt.Rows[i]["LicensePlate"].ToString());
                        vechile.PlanType = (dt.Rows[i]["PlanType"] == DBNull.Value ? "" : dt.Rows[i]["PlanType"].ToString());
                        vechile.ServiceName = (dt.Rows[i]["ServiceName"] == DBNull.Value ? "" : dt.Rows[i]["ServiceName"].ToString());
                        vechile.ServiceDescription = (dt.Rows[i]["ServiceDescription"] == DBNull.Value ? "" : dt.Rows[i]["ServiceDescription"].ToString());
                        vechile.ServiceAmount = (dt.Rows[i]["ServiceAmount"] == DBNull.Value ? 00 : (decimal)dt.Rows[i]["ServiceAmount"]);
                        vechile.RequestedServiceDate = (dt.Rows[i]["RequestedServiceDate"] == DBNull.Value ? "" : dt.Rows[i]["RequestedServiceDate"].ToString());
                        vechile.ActualServiceDate = (dt.Rows[i]["ActualServiceDate"] == DBNull.Value ? "" : dt.Rows[i]["ActualServiceDate"].ToString());
                        vechile.ServiceOutDate = (dt.Rows[i]["ServiceOutDate"] == DBNull.Value ? "" : dt.Rows[i]["ServiceOutDate"].ToString());
                        vechile.Status = (dt.Rows[i]["Status"] == DBNull.Value ? "" : dt.Rows[i]["Status"].ToString());
                        vechile.PaymentType = (dt.Rows[i]["PaymentType"] == DBNull.Value ? "" : dt.Rows[i]["PaymentType"].ToString());
                        vechile.Amount = (dt.Rows[i]["Amount"] == DBNull.Value ? 00 : (decimal)dt.Rows[i]["Amount"]);
                        vechile.Promocode_ReducedAmount = (dt.Rows[i]["Promocode_ReducedAmount"] == DBNull.Value ? "" : dt.Rows[i]["Promocode_ReducedAmount"].ToString());
                        vechile.PaymentStatus = (dt.Rows[i]["PaymentStatus"] == DBNull.Value ? "" : dt.Rows[i]["PaymentStatus"].ToString());
                        vechile.PaymentDate = (dt.Rows[i]["PaymentDate"] == DBNull.Value ? "" : dt.Rows[i]["PaymentDate"].ToString());
                        vechileList.Add(vechile);
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
                string SaveErrorLog = Data.Common.SaveErrorLog("GetVehiclePaymentDetails", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion
        
    }
}