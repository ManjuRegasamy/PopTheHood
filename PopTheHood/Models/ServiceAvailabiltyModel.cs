using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Models
{
    public class VehicleServiceHistoryDetails
    {
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string LocationID { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationFullAddress { get; set; }
        public int ServicePlanID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string PlanType { get; set; }
        public decimal ServiceAmount { get; set; }
        public string RequestedServiceDate { get; set; }
        public string ActualServiceDate { get; set; }
        public string ServiceOutDate { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public int ServicePriceChartId { get; set; }
        public int ScheduleID { get; set; }
    }


    public class ServicesModel
    {
        public int ServicePlanID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
