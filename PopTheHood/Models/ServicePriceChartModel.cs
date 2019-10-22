using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Models
{

    public class ServicePriceChartModel
    {
        public int ServicePriceChartId { get; set; }
        public int AvailableServiceID { get; set; }
        public int ServicePlanID { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        public string ServiceName { get; set; }
        public string PlanType { get; set; }
        public string Description { get; set; }
        public string BusinessCondition { get; set; }
        public string Notes { get; set; }
        //public int servicePlanID { get; set; }
    }

}
