﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopTheHood.Data;
using static Nexmo.Api.SMS;

namespace PopTheHood.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class OTPNotificationController : ControllerBase
    {

        private readonly IHostingEnvironment _env;

        public OTPNotificationController(IHostingEnvironment env)
        {
            _env = env;
        }


        #region SmsOTP
        [HttpGet, Route("SmsOTP")]
        public IActionResult SmsOTP(string PhoneNumber)
        {
            try
            {
                string OTPValue = Common.GenerateOTP();

                var results = "";
                //results = SmsNotification.SendMessage(PhoneNumber, "Hi User, your OTP is" + OTPValue + "and it's expiry time is 5 minutes.").ToString();
                //// results = SmsNotification.SendMessage(userlogin.PhoneNumber, "Hi User, your OTP is" + OTPValue + "and it's expiry time is 5 minutes.").Status.ToString();

                var SmsStatus = "";
                //if (results == "RanToCompletion")
                //{
                //    string SaveOtpValue = Data.Common.SaveOTP("4560123045", OTPValue, "Phone");
                //    SmsStatus = "Message sent successfully.";
                //}
                //else
                //{
                //    SmsStatus = "Message not sent..";
                //}

                string SaveOtpValue = Data.Common.SaveOTP(PhoneNumber, OTPValue, "Phone");
                if (SaveOtpValue == "Success")
                {
                    //SMSResponse results = SmsNotification.SendMessage(PhoneNumber, "Hi User, your OTP is " + OTPValue + " and it's expiry time is 5 minutes.");
                    results = SmsNotification.SendMessage(PhoneNumber, "Hi User, your OTP is " + OTPValue + " and it's expiry time is 5 minutes.").Status.ToString();
                    if (results == "RanToCompletion")
                    {
                        SmsStatus = "Message sent successfully.";
                    }
                    else
                    {
                        SmsStatus = "Message not sent..";
                    }

                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = SmsStatus, result = results, Status = "success" });
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Phone number not available", Status = "success" });
                }

            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SmsOTP", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
            }
        }
        #endregion

        #region EmailOTP
        [HttpGet, Route("EmailOTP")]
        public IActionResult EmailOTP(string emailid)
        {
            try
            {
                string res = "";
                var result = "";

                //var FilePath = _env.WebRootPath + Path.DirectorySeparatorChar.ToString()
                //+ "EmailView"
                //+ Path.DirectorySeparatorChar.ToString()
                //+ "EmailTemplate.html";
                //var ImagePath = _env.WebRootPath + Path.DirectorySeparatorChar.ToString()
                //+ "Images"
                //+ Path.DirectorySeparatorChar.ToString()
                //+ "PopTheHood_Logo.jpg";

                string OTPValue = Common.GenerateOTP();

                string SaveOtpValue = Data.Common.SaveOTP(emailid, OTPValue, "Email");

                if (SaveOtpValue == "Success")
                {
                    res = EmailSendGrid.Mail("chitrasubburaj30@gmail.com", emailid, "OTP Verification", "userlogin.Name", "Hello, your OTP is " + OTPValue + " and it's expiry time is 5 minutes.").Result; // "chitrasubburaj30@gmail.com",, FilePath
                    if (res == "Accepted")
                    {
                        result = "Mail sent successfully.";
                    }
                    else
                    {
                        result = "Bad Request";
                    }

                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = result, SaveOtpValues = SaveOtpValue, Status = "success" });
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Email Id not available", SaveOtpValues = SaveOtpValue, Status = "success" });
                }


                //var result = "";
                //if (res == "Accepted")
                //{
                //    result = "Mail sent successfully.";
                //}
                //else
                //{
                //    result = "Bad Request";
                //}

                //return StatusCode((int)HttpStatusCode.OK, new { Data = result, Status = "Success" });
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("EmailOTP", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
            }
        }
        #endregion

        #region OTPVerification
        [HttpPost, Route("OTPVerification")]
        public IActionResult OTPVerification(string OTP, string PhoneorEmail)
        {
            try
            {
                //DataTable dt = Data.Users.PhoneOrEmailVerification(PhoneorEmail, OTP);
                DateTime ExpiryDate = DateTime.Now;
                string Type = "";
                if(PhoneorEmail.Contains('@'))
                {
                    Type = "Email";
                }
                else
                {
                    Type = "Phone";
                }
                string row = Data.Users.PhoneOrEmailVerification(PhoneorEmail, OTP, Type);
                
                if (row == "OTP Verified")
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = "Verified Successfully", Status = "Success" });
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = row, Status = "Success" });
                }
                    
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("OTPVerification", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
            }
        }
        #endregion
    }
}