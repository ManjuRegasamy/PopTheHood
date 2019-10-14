using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PopTheHood.Models;
using System.Data;
using System.Net;
using PopTheHood.Data;

namespace PopTheHood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTAuthenticationController : ControllerBase
    {
       
        private IConfiguration _config;

        public JWTAuthenticationController(IConfiguration config)
        {
            _config = config;
        }


        private string GenerateJSONWebToken()  //Login userInfo
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            IConfigurationRoot configuration = builder.Build();
            var JwtKey = configuration.GetSection("Jwt").GetSection("Key").Value;

            var JwtIssuer = configuration.GetSection("Jwt").GetSection("Issuer").Value;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(JwtIssuer,
            JwtIssuer,
            null,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        #region GetUserLogin     
        // GET api/values
        [HttpPost, Route("Login")]
        public IActionResult Login([FromBody]Login userlogin)
        {
            //string GetConnectionString = UsersController.GetConnectionString();
            IActionResult response = Unauthorized();
            //var user = AuthenticateUser(login);



            List<UsersLogin> userList = new List<UsersLogin>();
            try
            {

                DataTable dt = Data.Users.Login(userlogin);

                if (dt.Rows.Count > 0)
                {
                    UsersLogin user = new UsersLogin();
                    user.UserId = (int)dt.Rows[0]["UserId"];
                    user.Email = (dt.Rows[0]["Email"] == DBNull.Value ? "" : dt.Rows[0]["Email"].ToString());
                    user.Name = (dt.Rows[0]["Name"] == DBNull.Value ? "" : dt.Rows[0]["Name"].ToString());
                    user.PhoneNumber = (dt.Rows[0]["PhoneNumber"] == DBNull.Value ? "" : dt.Rows[0]["PhoneNumber"].ToString());
                    user.SourceofReg = (dt.Rows[0]["SourceofReg"] == DBNull.Value ? "" : dt.Rows[0]["SourceofReg"].ToString());
                    user.IsPromoCodeApplicable = (dt.Rows[0]["IsPromoCodeApplicable"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsPromoCodeApplicable"]);
                    user.IsEmailVerified = (dt.Rows[0]["IsEmailVerified"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsEmailVerified"]);
                    user.IsPhoneNumVerified = (dt.Rows[0]["IsPhoneNumVerified"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsPhoneNumVerified"]);
                    user.CreatedDate = (dt.Rows[0]["CreatedDate"] == DBNull.Value ? "" : dt.Rows[0]["CreatedDate"].ToString());
                    user.Role = (dt.Rows[0]["Role"] == DBNull.Value ? "" : dt.Rows[0]["Role"].ToString());
                    //user.ModifiedDate = (dt.Rows[0]["ModifiedDate"] == DBNull.Value ? "" : dt.Rows[0]["ModifiedDate"].ToString());
                    //user.IsDeleted = (dt.Rows[0]["IsDeleted"] == DBNull.Value ? false : (bool)dt.Rows[0]["IsDeleted"]);
                    userList.Add(user);

                    var tokenstr = GenerateJSONWebToken();
                    var encrypt = Common.EncryptData(tokenstr);
                    //var decrypt = ss.DecryptData(encrypt);    , DecryptedText = decrypt
                    return StatusCode((int)HttpStatusCode.OK, new { Data = userList, TokenString = tokenstr, EncryptedTokenString = encrypt, Status = "Success" });
                }
                else
                {
                    string SaveErrorLog = Data.Common.SaveErrorLog("Login", "Invalid User");

                    return StatusCode((int)HttpStatusCode.Unauthorized, new { Data = "Invalid User", Status = "Error" });
                }

            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("Login", e.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError, new { Data = e.Message.ToString(), Status = "Error" });
            }
        }
        #endregion


    }
}