using Microsoft.ApplicationBlocks.Data;
using PopTheHood.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Data
{
    public class PromoCode
    {

        public static string SavePromoCode(PromoCodeModel promocode)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@PromocodeText", promocode.PromocodeText));
                parameters.Add(new SqlParameter("@PromoType", promocode.PromoType));
                parameters.Add(new SqlParameter("@PromocodeValue", promocode.PromocodeValue));
                parameters.Add(new SqlParameter("@NumberofUsePerUser", promocode.NumberofUsePerUser));
                parameters.Add(new SqlParameter("@StartDate", promocode.StartDate));
                parameters.Add(new SqlParameter("@ExpiryDate", promocode.ExpiryDate));


                using (DataSet dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spSavePromoCode", parameters.ToArray()))
                {
                    string rowsAffected = dt.Tables[0].Rows[0]["Status"].ToString();

                    return rowsAffected;
                }
            }
            catch (Exception e)
            {
                //loggerErr.Error(e.Message + " - " + e.StackTrace);
                throw e;
            }

        }

        public static DataTable ApplyPromoCode(PromoCodes promocode)
        {
            try
            {
                string ConnectionString = Common.GetConnectionString();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@PromocodeText", promocode.PromocodeText));
                parameters.Add(new SqlParameter("@UserId", promocode.UserId));
                //Execute the query
                using (DataTable dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spApplyPromoCode", parameters.ToArray()).Tables[0])
                {
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
