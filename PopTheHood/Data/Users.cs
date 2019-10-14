using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
using PopTheHood.Models;

namespace PopTheHood.Data
{
    public class Users
    {
        public static DataTable Login(Login userlogin)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                //SqlParameter[] parameters =
                //{
                //    new SqlParameter("@Email", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 10, 0, "Email", DataRowVersion.Proposed, userlogin.Email),
                //    new SqlParameter("@Password", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 10, 0, "Password", DataRowVersion.Proposed, userlogin.Password)
                //};
                var encryptPassword = Common.EncryptData(userlogin.Password);

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Email", userlogin.Email));
                parameters.Add(new SqlParameter("@Password", encryptPassword));

                DataTable dt = new DataTable();
                using (dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spLoginUser", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static string UpdatePassword(Login userlogin)
        {
            //SqlParameter[] parameters =
            //{
            //    new SqlParameter("@Email", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 10, 0, "Email", DataRowVersion.Proposed, userlogin.Email),
            //    new SqlParameter("@Password", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 10, 0, "Password", DataRowVersion.Proposed, userlogin.Password)
            //};
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Email", userlogin.Email));
            parameters.Add(new SqlParameter("@Password", userlogin.Password));

            try
            {
                string ConnectionString = Common.GetConnectionString();
                //Execute the query
                //using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spUpdatePassword", parameters).Tables[0])
                //{
                string rowsAffected = SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spUpdatePassword", parameters.ToArray()).ToString();
                return rowsAffected;
                //}
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }
        }

        public static DataTable GetAllUserList(string Search)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                //Create the parameters in the SqlParameter array
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Search", Search));

                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetUserList", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static int DeleteUser(int UserID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserID", UserID));

            try
            {
                string ConnectionString = Common.GetConnectionString();

                int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spDeleteUser", parameters.ToArray());
                return rowsAffected;
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }
        }

        public static int UpdateVerificationStatus(int UserId, bool Status, IEnumerable<Common.Source> Source)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", UserId));
                parameters.Add(new SqlParameter("@Status", Status));
                parameters.Add(new SqlParameter("@UpdateStatus", Source));
                            
                int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spUpdatePhoneEmailStatus", parameters.ToArray());
                return rowsAffected;
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }
        }

        public static DataTable GetUserById(int UserId)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                //Create the parameters in the SqlParameter array
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", UserId));

                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spGetUserById", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static string SaveUser(UsersLogin userlogin, string Action)
        {
            //int UserId, string Name, string PhoneNumber, string Email, string Password, string SourceofReg, bool IsEmailVerified, bool IsPhoneNumVerified,
            //bool IsPromoCodeApplicable, string Action,

            try
            {
                string ConnectionString = Common.GetConnectionString();

                var encryptPassword = Common.EncryptData(userlogin.Password);

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@UserId", userlogin.UserId));
                parameters.Add(new SqlParameter("@Name", userlogin.Name));
                parameters.Add(new SqlParameter("@PhoneNumber", userlogin.PhoneNumber));
                parameters.Add(new SqlParameter("@Email", userlogin.Email));
                parameters.Add(new SqlParameter("@Password", encryptPassword));
                parameters.Add(new SqlParameter("@SourceofReg", userlogin.SourceofReg));
                parameters.Add(new SqlParameter("@IsEmailVerified", userlogin.IsEmailVerified));
                parameters.Add(new SqlParameter("@IsPhoneNumVerified", userlogin.IsPhoneNumVerified));
                parameters.Add(new SqlParameter("@IsPromoCodeApplicable", userlogin.IsPromoCodeApplicable));
                parameters.Add(new SqlParameter("@Role", userlogin.Role));
                parameters.Add(new SqlParameter("@Action", Action));

                string rowsAffected = SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, "spSaveUser", parameters.ToArray()).ToString();
                //SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spSaveUser", parameters.ToArray());
                return rowsAffected;
                //using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spSaveUser", parameters.ToArray()).Tables[0])
                //{
                //    int rowsAffected = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "spUpdatePhoneEmailStatus", parameters.ToArray());
                //    return rowsAffected;
                //}
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }
            
        }
    }
}
