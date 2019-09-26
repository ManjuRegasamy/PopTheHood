﻿using System;
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NLog;
using PopTheHood.Data;
using PopTheHood.Models;

namespace PopTheHood.Controllers
{
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
                    return StatusCode((int)HttpStatusCode.OK, new { Data = "Updated Successfully", Status = "Success" });
                }
                else
                {
                    //return "Invalid user";
                   return StatusCode((int)HttpStatusCode.Unauthorized, new { Data = "Account not exist", Status = "Error" });
                }
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("UpdatePassword", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }
            
        }
        #endregion

        #region GetAllUserList
        [HttpGet, Route("GetAllUserList")]
        public IActionResult GetAllUserList(string Search)
        {
            //string GetConnectionString = UsersController.GetConnectionString();
            //string GetConnectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            List<UsersLogin> userList = new List<UsersLogin>();
            try
            {
                if (Search == null)
                {
                    Search = "";
                }
                DataTable dt = Data.Users.GetAllUserList(Search);

                if(dt.Rows.Count > 0)
                { 
                   for(int i=0; i < dt.Rows.Count;i++)
                    {                        
                        UsersLogin user = new UsersLogin();
                        user.UserId =(int)dt.Rows[i]["UserId"];
                        user.Email = (dt.Rows[i]["Email"] == DBNull.Value ? "" : dt.Rows[i]["Email"].ToString());
                        user.Name = (dt.Rows[i]["Name"] == DBNull.Value ? "" : dt.Rows[i]["Name"].ToString());
                        user.PhoneNumber = (dt.Rows[i]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[i]["PhoneNumber"].ToString());
                        user.SourceofReg = (dt.Rows[i]["SourceofReg"] == DBNull.Value ? "" : dt.Rows[i]["SourceofReg"].ToString()); 
                        user.IsPromoCodeApplicable = (dt.Rows[i]["IsPromoCodeApplicable"] == DBNull.Value ? false : (bool) dt.Rows[i]["IsPromoCodeApplicable"]);
                        user.IsEmailVerified = (dt.Rows[i]["IsEmailVerified"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsEmailVerified"]);
                        user.IsPhoneNumVerified = (dt.Rows[i]["IsPhoneNumVerified"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsPhoneNumVerified"]);
                        user.CreatedDate = (dt.Rows[i]["CreatedDate"] == DBNull.Value ? "" : dt.Rows[i]["CreatedDate"].ToString());
                        //user.ModifiedDate = (dt.Rows[i]["ModifiedDate"] == DBNull.Value ? "" : dt.Rows[i]["ModifiedDate"].ToString());
                        //user.IsDeleted = (dt.Rows[0]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsDeleted"]);
                        userList.Add(user);
                    }

                    return StatusCode((int)HttpStatusCode.OK, new { Data = userList, Status = "Success" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = userList, Status = "Success" });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetAllUserList", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }
        }
        #endregion
        
        #region DeleteUser
        [HttpDelete, Route("DeleteUser/{UserId}")]
        public IActionResult DeleteUser(int UserId)
        {
            //string connectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            UsersLogin users = new UsersLogin();
            try
            {
                int row = Data.Users.DeleteUser(UserId);

                if (row > 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = "Deleted Successfully", Status = "Success" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Error while Deleting the record", Status = "Error" });
                }

            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("DeleteUser", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" });
            }
        }
        #endregion
        
        #region UpdatePhoneEmailStatus
        [HttpPut, Route("UpdatePhoneEmailStatus/{UserId}/{UpdateStatus}/{Status}")]
        public IActionResult UpdatePhoneEmailStatus(int UserId, bool Status, string UpdateStatus)
        {
            //string connectionString = configuration.GetSection("ConnectionString").GetSection("DefaultConnection").Value;
            UsersLogin users = new UsersLogin();
            try
            {
                int row = Data.Users.UpdatePhoneEmailStatus(UserId, Status, UpdateStatus);

                if (row > 0)
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = "Updated Successfully", Status = "Success" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = "Error while Update Phone/Email status", Status = "Error" });
                }
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("UpdatePhoneEmailStatus", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message, Status = "Error" }); 
            }
        }
        #endregion
        
        #region GetUserById
        [HttpGet, Route("GetUserById/{UserId}")]
        public IActionResult GetUserById(int UserId)
        {
            //string GetConnectionString = UsersController.GetConnectionString();
            List<UsersLogin> userList = new List<UsersLogin>();
            try
            {
                DataTable dt = Data.Users.GetUserById(UserId);

                if(dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        UsersLogin user = new UsersLogin();
                        user.UserId = (int)dt.Rows[i]["UserId"];
                        user.Email = (dt.Rows[i]["Email"] == DBNull.Value ? "" : dt.Rows[i]["Email"].ToString());
                        user.Name = (dt.Rows[i]["Name"] == DBNull.Value ? "" : dt.Rows[i]["Name"].ToString());
                        //user.Password = (dt.Rows[i]["Password"] == DBNull.Value ? "" : dt.Rows[i]["Password"].ToString());
                        user.PhoneNumber = (dt.Rows[i]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[i]["PhoneNumber"].ToString());
                        user.SourceofReg = (dt.Rows[i]["SourceofReg"] == DBNull.Value ? "" : dt.Rows[i]["SourceofReg"].ToString());
                        user.IsPromoCodeApplicable = (dt.Rows[i]["IsPromoCodeApplicable"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsPromoCodeApplicable"]);
                        user.IsEmailVerified = (dt.Rows[i]["IsEmailVerified"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsEmailVerified"]);
                        user.IsPhoneNumVerified = (dt.Rows[i]["IsPhoneNumVerified"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsPhoneNumVerified"]);
                        user.CreatedDate = (dt.Rows[i]["CreatedDate"] == DBNull.Value ? "" : dt.Rows[i]["CreatedDate"].ToString());
                        //user.ModifiedDate = (dt.Rows[i]["ModifiedDate"] == DBNull.Value ? "" : dt.Rows[i]["ModifiedDate"].ToString());
                        //user.IsDeleted = (dt.Rows[i]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsDeleted"]);
                        userList.Add(user);
                    }
                    return StatusCode((int)HttpStatusCode.OK, new { Data = userList, Status = "Success" });
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { Data = userList, Status = "Success" });
                }
                
            }
            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("GetUserById", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        #endregion

        #region SaveUser
        [HttpPost, Route("SaveUser")]
        [AllowAnonymous]
        public IActionResult SaveUser([FromBody]UsersLogin userlogin, string Action)  //int UserId, string Name, string PhoneNumber, string Email, string Password, string SourceofReg, bool IsEmailVerified, bool IsPhoneNumVerified,
            //bool IsPromoCodeApplicable, string Action
        {
            //string GetConnectionString = UsersController.GetConnectionString();
            List<UsersLogin> userList = new List<UsersLogin>();
            try
            {
                string row = Data.Users.SaveUser(userlogin , Action == null ? "" : Action);
                string res = "";
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

                    res = EmailSendGrid.Mail("manjurengasamy77@gmail.com", "chitrasubburaj30@gmail.com", "User Registration", FilePath, ImagePath).Result; // "chitrasubburaj30@gmail.com",
                    var result = "";
                    if(res == "Accepted")
                    {
                        result = "Mail send successfully.";
                    }
                    else
                    {
                        result = "Bad Request";
                    }
                    return StatusCode((int)HttpStatusCode.OK, new { Data = "Saved Successfully", Mailing = result, Status = "Success" });
                }
                else
                {
                    //return "Invalid user";
                    return StatusCode((int)HttpStatusCode.Unauthorized, new { Data = "User already exist", Status = "Error" });
                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("SaveUser", e.Message.ToString());

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
            }
        }

        #endregion

    }
}