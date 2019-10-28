using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nexmo.Api;
using NLog;
using PopTheHood.Data;
using PopTheHood.Models;

namespace PopTheHood.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : Controller
    {

        private readonly IHostingEnvironment _env;

        public UsersController(IHostingEnvironment env)
        {
            _env = env;
        }



        #region UpdatePassword    
        [HttpPut, Route("UpdatePassword")]
        [AllowAnonymous]
        public IActionResult UpdatePassword([FromBody]Login userlogin)
        {
            //string connectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            UsersLogin user = new UsersLogin();
            try
            {
                string row = Data.Users.UpdatePassword(userlogin);

                if (row == "Success")
                {
                    return StatusCode((int)HttpStatusCode.OK, "Updated Successfully" );
                }
                else
                {
                    //return "Invalid user";
                   return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = "Account not exist" } });
                }
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("UpdatePassword", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
            
        }
        #endregion

        #region ForgetPassword
        [HttpGet, Route("ForgetPassword")]
        [AllowAnonymous]
        public IActionResult ForgetPassword(string PhoneOrEmail)
        {
            try
            {
                string Result = Common.SendOTP(PhoneOrEmail, "ForgetPassword");
                if (Result == "Mail sent successfully.")
                {
                    return StatusCode((int)HttpStatusCode.OK, "OTP Sent to Email");
                }
                else
                {
                    string SaveErrorLog = Data.Common.SaveErrorLog("ForgetPassword", Result);

                    return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = Result } });
                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("ForgetPassword", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
            
            
        }
        #endregion

        #region Users
        [HttpGet, Route("Users")]
        public IActionResult GetAllUserList()
        {
            //string GetConnectionString = UsersController.GetConnectionString();
            //string GetConnectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            List<UsersLogin> userList = new List<UsersLogin>();
            try
            {
               
                DataTable dt = Data.Users.GetAllUserList();

                if(dt.Rows.Count > 0)
                { 
                   for(int i=0; i < dt.Rows.Count;i++)
                    {                        
                        UsersLogin user = new UsersLogin();

                        var DecryptPassword = Common.DecryptData(dt.Rows[i]["Password"] == DBNull.Value ? "" : dt.Rows[i]["Password"].ToString());

                        user.UserId =(int)dt.Rows[i]["UserId"];
                        user.Email = (dt.Rows[i]["Email"] == DBNull.Value ? "" : dt.Rows[i]["Email"].ToString());
                        user.Password = DecryptPassword;    // (dt.Rows[i]["Password"] == DBNull.Value ? "" : dt.Rows[i]["Password"].ToString());
                        user.Name = (dt.Rows[i]["Name"] == DBNull.Value ? "" : dt.Rows[i]["Name"].ToString());
                        user.PhoneNumber = (dt.Rows[i]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[i]["PhoneNumber"].ToString());
                        user.SourceofReg = (dt.Rows[i]["SourceofReg"] == DBNull.Value ? "" : dt.Rows[i]["SourceofReg"].ToString()); 
                        user.IsPromoCodeApplicable = (dt.Rows[i]["IsPromoCodeApplicable"] == DBNull.Value ? false : (bool) dt.Rows[i]["IsPromoCodeApplicable"]);
                        user.IsEmailVerified = (dt.Rows[i]["IsEmailVerified"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsEmailVerified"]);
                        user.IsPhoneNumVerified = (dt.Rows[i]["IsPhoneNumVerified"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsPhoneNumVerified"]);
                        user.CreatedDate = (dt.Rows[i]["CreatedDate"] == DBNull.Value ? "" : dt.Rows[i]["CreatedDate"].ToString());
                        user.Role = (dt.Rows[i]["Role"] == DBNull.Value ? "" : dt.Rows[i]["Role"].ToString());
                        user.VehicleCount= (int)dt.Rows[i]["VehicleCount"];
                        //user.ModifiedDate = (dt.Rows[i]["ModifiedDate"] == DBNull.Value ? "" : dt.Rows[i]["ModifiedDate"].ToString());
                        //user.IsDeleted = (dt.Rows[0]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsDeleted"]);
                        userList.Add(user);
                        DecryptPassword = "";
                    }

                    return StatusCode((int)HttpStatusCode.OK, userList );
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK,  userList);
                }
                 
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("Users", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion
        
        #region DeleteUser
        [HttpDelete, Route("User/{UserId}")]
        public IActionResult DeleteUser(int UserId)
        {
            //string connectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            UsersLogin users = new UsersLogin();
            try
            {
                int row = Data.Users.DeleteUser(UserId);

                if (row > 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, "Deleted Successfully");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "Error while Deleting the record" } });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("DeleteUser", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion

        #region UpdateVerificationStatus
        [HttpPut, Route("UpdateVerificationStatus/{UserId}/{Source}/{Status}")]
        public IActionResult UpdateVerificationStatus(int UserId, bool Status, RegSource Source)
        {
            //string connectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            UsersLogin users = new UsersLogin();
            try
            {
                int row = Data.Users.UpdateVerificationStatus(UserId, Status, Source.ToString());

                if (row > 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, "Updated Successfully");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = "Error while Update Phone/Email status" } });
                }
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("UpdatePhoneEmailStatus", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } }); 
            }
        }
        #endregion
        
        #region User
        [HttpGet, Route("User/{UserId}")]
        public IActionResult GetUserById(int UserId)
        {
            //string GetConnectionString = UsersController.GetConnectionString();
            List<UsersLogin> userList = new List<UsersLogin>();
            try
            {
                DataTable dt = Data.Users.GetUserById(UserId);
                UsersLogin user = new UsersLogin();

                if (dt.Rows.Count > 0)
                {
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    var DecryptPassword = Common.DecryptData(dt.Rows[0]["Password"] == DBNull.Value ? "" : dt.Rows[0]["Password"].ToString()); 
                        user.UserId = (int)dt.Rows[0]["UserId"];
                        user.Email = (dt.Rows[0]["Email"] == DBNull.Value ? "" : dt.Rows[0]["Email"].ToString());
                        user.Name = (dt.Rows[0]["Name"] == DBNull.Value ? "" : dt.Rows[0]["Name"].ToString());
                        user.Password = DecryptPassword; //(dt.Rows[0]["Password"] == DBNull.Value ? "" : dt.Rows[0]["Password"].ToString());
                        user.PhoneNumber = (dt.Rows[0]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[0]["PhoneNumber"].ToString());
                        user.SourceofReg = (dt.Rows[0]["SourceofReg"] == DBNull.Value ? "" : dt.Rows[0]["SourceofReg"].ToString());
                        user.IsPromoCodeApplicable = (dt.Rows[0]["IsPromoCodeApplicable"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsPromoCodeApplicable"]);
                        user.IsEmailVerified = (dt.Rows[0]["IsEmailVerified"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsEmailVerified"]);
                        user.IsPhoneNumVerified = (dt.Rows[0]["IsPhoneNumVerified"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsPhoneNumVerified"]);
                        user.CreatedDate = (dt.Rows[0]["CreatedDate"] == DBNull.Value ? "" : dt.Rows[0]["CreatedDate"].ToString());
                        user.Role = (dt.Rows[0]["Role"] == DBNull.Value ? "" : dt.Rows[0]["Role"].ToString());
                        //user.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);
                        userList.Add(user);
                    //}
                    return StatusCode((int)HttpStatusCode.OK, new { user });
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { });
                }
                
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("User", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message } });
            }
        }
        #endregion

        #region SaveUser
        [HttpPost, Route("User")]
        [AllowAnonymous]
        public IActionResult SaveUser([FromBody]UsersLogin userlogin)  //int UserId, string Name, string PhoneNumber, string Email, string Password, string SourceofReg, bool IsEmailVerified, bool IsPhoneNumVerified,
            //bool IsPromoCodeApplicable, string Action
        {
            //string GetConnectionString = UsersController.GetConnectionString();
            List<UsersLogin> userList = new List<UsersLogin>();
            string Action = "Add";
            if (userlogin.Role != "Admin")
            {
                userlogin.Role = "User";
            }
            try
            {
                string row = Data.Users.SaveUser(userlogin , Action);
                string res = "";
                string smsres = "";

                if (row == "Success")
                {

                    var FilePath = _env.WebRootPath + Path.DirectorySeparatorChar.ToString()
                    + "EmailView"
                    + Path.DirectorySeparatorChar.ToString()
                    + "EmailTemplate.html";

                    var ImagePath = _env.WebRootPath + Path.DirectorySeparatorChar.ToString()
                    + "Images"
                    + Path.DirectorySeparatorChar.ToString()
                    + "PopTheHood_Logo.jpg";

                    string OTPValue = Common.GenerateOTP();

                    //res = EmailSendGrid.Mail("chitrasubburaj30@gmail.com", "murukeshs@apptomate.co", "User Registration", userlogin.Name, "Hi " + userlogin.Name + " , your OTP is " + OTPValue + " and it's expiry time is 5 minutes.", FilePath).Result; // "chitrasubburaj30@gmail.com",


                    var results = "";
                    //results = SmsNotification.SendMessage("7010214439", "Hi User, your OTP is" + OTPValue + "and it's expiry time is 5 minutes.").ToString();

                    // results = SmsNotification.SendMessage(userlogin.PhoneNumber, "Hi User, your OTP is" + OTPValue + "and it's expiry time is 5 minutes.").Status.ToString();

                    //var client = new Client(creds: new Nexmo.Api.Request.Credentials
                    //{
                    //    ApiKey = "5d5eb59f",
                    //    ApiSecret = "xFT1BuHaxN6wzA8M"
                    //});
                    //var results = client.SMS.Send(new SMS.SMSRequest
                    //{
                    //    from = "7708178085",
                    //    to = "7010214439",
                    //    text = "Hi User, your OTP is" + OTPValue
                    //});

                    var SmsStatus = "";
                    if (results == "RanToCompletion")
                    {
                        string SaveOtpValue = Data.Common.SaveOTP(userlogin.PhoneNumber, OTPValue, "Phone");
                        SmsStatus = "Message sent successfully.";
                    }
                    else
                    {
                        SmsStatus = "Message not sent..";
                    }

                    var result = "";
                    if (res == "Accepted")
                    {
                        string SaveOtpValue = Data.Common.SaveOTP(userlogin.Email, OTPValue, "Email");
                        result = "Mail sent successfully.";
                    }
                    else
                    {
                        result = "Bad Request";
                    }
                    //return StatusCode((int)HttpStatusCode.OK, new { Data = "Saved Successfully", Mailing = result, SMS = results, SMSSTATUS = SmsStatus, OTP = OTPValue, Status = "Success" });
                    return StatusCode((int)HttpStatusCode.OK, "Saved Successfully");
                }
                else
                {
                    //return "Invalid user";
                    //var expected = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { foo = "abcd" }));
                    return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = "invalid user" } });
                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SaveUser", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }

        #endregion

        #region UpdateUser
        [HttpPut, Route("User")]
        public IActionResult UpdateUser([FromBody]UsersLogin userlogin)
        {
            List<UsersLogin> userList = new List<UsersLogin>();
            string Action = "Update";
            try
            {
                string row = Data.Users.SaveUser(userlogin, Action);

                if (row == "Success")
                {
                    //string OTPValue = Common.GenerateOTP();
                    
                    //var results = "";
                  
                    //var SmsStatus = "";
                    //if (results == "RanToCompletion")
                    //{
                    //    string SaveOtpValue = Data.Common.SaveOTP("4560123045", OTPValue, "Phone");
                    //    SmsStatus = "Message sent successfully.";
                    //}
                    //else
                    //{
                    //    SmsStatus = "Message not sent..";
                    //}

                    //var result = "";
                    //if (res == "Accepted")
                    //{
                    //    string SaveOtpValue = Data.Common.SaveOTP("murukeshs@apptomate.co", OTPValue, "Email");
                    //    result = "Mail sent successfully.";
                    //}
                    //else
                    //{
                    //    result = "Bad Request";
                    //}
                    return StatusCode((int)HttpStatusCode.OK, "Updated Successfully" );
                }
                else
                {
                    //return "Invalid user";
                    return StatusCode((int)HttpStatusCode.Forbidden, new { error = new { message = "Invalid User" } });
                }
            }

            catch(Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("UpdateUser", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { error = new { message = e.Message.ToString() } });
            }
        }

        #endregion
        
    }
}
