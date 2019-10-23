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
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();

            List<DashboardRecords> reportList = new List<DashboardRecords>();
            List<DashboardMonthlyReport> MonthlyreportList = new List<DashboardMonthlyReport>();
            List<DashboardGeneralServicereport> GeneralServiceList = new List<DashboardGeneralServicereport>();
            List<VehicleScheduledReport> VehicleScheduledListForADay = new List<VehicleScheduledReport>();
            List<VehicleScheduledReport> VehicleScheduledForAWeek = new List<VehicleScheduledReport>();
            
            try
            { 

                if (ds.Tables.Count > 0)
                {
                    dt1 = ds.Tables[0];
                    dt2 = ds.Tables[1];
                    dt3 = ds.Tables[2];
                    dt4 = ds.Tables[3];
                    dt5 = ds.Tables[4];

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
                            DashboardMonthlyReport Monthlyreport = new DashboardMonthlyReport();
                            Monthlyreport.MonthList = dt2.Rows[i]["MonthwiseCount"].ToString();
                            Monthlyreport.MonthwiseCount = (int)dt2.Rows[i]["TotalService"];
                            MonthlyreportList.Add(Monthlyreport);
                        }
                    }

                    if (dt3.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            DashboardGeneralServicereport GeneralServicereport = new DashboardGeneralServicereport();
                            GeneralServicereport.ServiceStatus = dt3.Rows[i]["ServiceStatus"].ToString();
                            GeneralServicereport.ServicesCount = (int)dt3.Rows[i]["ServicesCount"];
                            GeneralServiceList.Add(GeneralServicereport);
                        }
                    }

                    if (dt4.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt4.Rows.Count; i++)
                        {
                            VehicleScheduledReport vehicles = new VehicleScheduledReport();
                            vehicles.LicensePlate = dt4.Rows[i]["LicensePlate"].ToString();
                            vehicles.Make = dt4.Rows[i]["Make"].ToString();
                            vehicles.Name = dt4.Rows[i]["Name"].ToString();
                            vehicles.PhoneNumber = dt4.Rows[i]["PhoneNumber"].ToString();
                            vehicles.LocationFullAddress = dt4.Rows[i]["LocationFullAddress"].ToString();
                            vehicles.CityName = dt4.Rows[i]["CityName"].ToString();
                            VehicleScheduledListForADay.Add(vehicles);
                        }
                    }

                    if (dt5.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {
                            VehicleScheduledReport vehicles = new VehicleScheduledReport();
                            vehicles.LicensePlate = dt5.Rows[i]["LicensePlate"].ToString();
                            vehicles.RequestServiceDate = dt5.Rows[i]["RequestServiceDate"].ToString();
                            VehicleScheduledForAWeek.Add(vehicles);

                        }
                    }

                    return StatusCode((int)HttpStatusCode.OK, new { report, MonthlyreportList, GeneralServiceList, VehicleScheduledListForADay, VehicleScheduledForAWeek });
                    
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