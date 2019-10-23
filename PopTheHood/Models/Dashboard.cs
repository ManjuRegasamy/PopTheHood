using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Models
{
    public class Dashboard
    {
        public class DashboardRecords
        {
            public int Users { get; set; }
            public int Vehicles { get; set; }
            public int Services { get; set; }            
        }

        public class DashboardMonthlyReport
        {
            public string MonthList { get; set; }
            public int MonthwiseCount { get; set; }
        }

        public class DashboardGeneralServicereport
        {
            public string ServiceStatus { get; set; }
            public int ServicesCount { get; set; }
        }

        public class VehicleScheduledReport
        {
            public string LicensePlate { get; set; }
            public string Make { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string LocationFullAddress { get; set; }
            public string CityName { get; set; }
            public string RequestServiceDate { get; set; }
        }
    }
}
