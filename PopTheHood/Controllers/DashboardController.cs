using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static PopTheHood.Models.Dashboard;

namespace PopTheHood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        #region Dashboard
        [HttpGet, Route("Dashboard")]

        public IActionResult GetDashboard()
        {
            DataSet ds = Data.Dashboard.GetDashboard();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();            
            
            List<DashboardRecords> reportList = new List<DashboardRecords>();
            List<DashboardRecords> MonthlyreportList = new List<DashboardRecords>();

            try
            { 

                if (ds.Tables.Count > 0)
                {
                    dt1 = ds.Tables[0];
                    dt2 = ds.Tables[1];
                    DashboardRecords report = new DashboardRecords();

                    if (dt1.Rows.Count > 0)
                    {                        
                        report.Users = (int)dt1.Rows[0]["Count"];
                        report.Vehicles = (int)dt1.Rows[1]["Count"];
                        report.Services = (int)dt1.Rows[2]["Count"];
                        reportList.Add(report);                        
                    }
                    
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            DashboardRecords Monthlyreport = new DashboardRecords();
                            Monthlyreport.MonthList = dt2.Rows[i]["MonthwiseCount"].ToString();
                            Monthlyreport.MonthwiseCount = (int)dt2.Rows[i]["TotalService"];
                            MonthlyreportList.Add(Monthlyreport);
                        }
                    }
                    return StatusCode((int)HttpStatusCode.OK, new { report, MonthlyreportList });
                }

                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new { });
                }
            }

            catch (Exception e)
            {
                string SaveErrorLog = Data.Common.SaveErrorLog("Dashboard", e.Message);
                return StatusCode((int) HttpStatusCode.InternalServerError, new { error = new { message = e.Message} });
            }
        }

        
        #endregion
    }
}