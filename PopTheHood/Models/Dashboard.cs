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
            public string MonthList { get; set; }
            public int MonthwiseCount { get; set; }
        }
    }
}
