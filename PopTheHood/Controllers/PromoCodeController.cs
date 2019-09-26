using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PopTheHood.Models;

namespace PopTheHood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PromoCodeController : ControllerBase
    {
       
        #region SavePromoCode
        [HttpPost, Route("SavePromoCode")]
        public IActionResult SavePromoCode([FromBody]PromoCodeModel promocode)
        {
            //string connectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            try
            {
                string row = Data.PromoCode.SavePromoCode(promocode);

                if (row == "Success")
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = "Saved Successfully", Status = "Success" });
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = row, Status = "Success" });
                }
            }


            catch (Exception e)
            {              
                if (e.Message.Contains("UNIQUE KEY constraint") == true)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "PromoCode is already exists", Status = "Error" });
                }
                else
                {
                    string SaveErrorLog = Data.Common.SaveErrorLog("SavePromoCode", e.Message);
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
                }               
            }
        }
        #endregion

        #region ApplyPromoCode
        [HttpPost, Route("ApplyPromoCode")]
        public IActionResult ApplyPromoCode([FromBody]PromoCodes promocode)
        {
            List<PromoCodeList> promoCode = new List<PromoCodeList>();
            //string GetConnectionString = PromoCodeController.GetConnectionString();
           // string GetConnectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;

            try
            {
               DataSet ds = Data.PromoCode.ApplyPromoCode(promocode).DataSet;

                string rowsAffected = ds.Tables[0].Rows[0]["Status"].ToString();

                if (rowsAffected == "Success")
                {
                    DataTable dt = ds.Tables[1];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            PromoCodeList promo = new PromoCodeList();
                            promo.PromocodeID = (int)dt.Rows[i]["PromocodeID"];
                            promo.PromocodeText = (dt.Rows[i]["PromocodeText"] == DBNull.Value ? "" : dt.Rows[i]["PromocodeText"].ToString());
                            promo.PromoType = (dt.Rows[i]["PromoType"] == DBNull.Value ? "" : dt.Rows[i]["PromoType"].ToString());
                            promo.PromocodeValue = (dt.Rows[i]["PromocodeValue"] == DBNull.Value ? 0 : (decimal)dt.Rows[i]["PromocodeValue"]);
                            //promo.NumberofUsePerUser = (dt.Rows[i]["NumberofUsePerUser"] == DBNull.Value ? 0 : (int)dt.Rows[i]["NumberofUsePerUser"]);
                            promo.UserId = (dt.Rows[i]["UserId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["UserId"]);
                            promo.StartDate = (dt.Rows[i]["StartDate"] == DBNull.Value ? "" : dt.Rows[i]["StartDate"].ToString());
                            promo.ExpiryDate = (dt.Rows[i]["ExpiryDate"] == DBNull.Value ? "" : dt.Rows[i]["ExpiryDate"].ToString());

                            promoCode.Add(promo);
                        }

                        return StatusCode((int)HttpStatusCode.OK, new { Data = promoCode, Status = "Success" });
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.OK, new { Data = promoCode, Status = "Success" });
                    }
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = rowsAffected, Status = "Error" });
                }


            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("ApplyPromoCode", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }
        }
        #endregion


    }
}