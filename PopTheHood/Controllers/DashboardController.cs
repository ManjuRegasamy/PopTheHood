using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        //#region Dashboard
        //[HttpGet, Route("Dashboard")]

        //public IActionResult GetDashboard()
        //{
        //    DataSet ds = Data.Dashboard.GetDashboard();
        //    DashboardRecords report = new DashboardRecords();

        //    //for (int i = 0; i <= ds.Tables.Count; i++)
        //    //{

        //    //   // report.Users = (int)dt.Rows[0]["UserId"];
        //    //}
        //    //DataTable dt = ds.Tables[i];

        //    //var TotalUser = ds.Tables[0];
        //    //var TotalVehicle = ds.Tables[1];
        //    //var TotalService = ds.Tables[2];
        //    //var Monthwise = ds.Tables[3];

        //    return "";
        //}
        //#endregion
    }
}