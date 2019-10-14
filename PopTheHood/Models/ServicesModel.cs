using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Models
{
   

    public class ServiceDetails : ServicePriceChartModel
    {
        public int ServiceID { get; set; }
        public int ServicePlanID { get; set; }
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public int RemainderMinutes { get; set; }
        public int LocationID { get; set; }
        public bool IsTeamsandConditionsAccepted { get; set; }
        [DefaultValue(false)]
        public bool? PromoCodeApplied { get; set; }
        public int ScheduleID { get; set; }
        public string Status { get; set; }
        public string RequestedServiceDate { get; set; }
        public string ActualServiceDate { get; set; }
        public string ServiceOutDate { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string PlanType { get; set; }
        public decimal Price { get; set; }
    }

    public class ServiceLocation
    {
        public int UserId { get; set; }
       // public int ServicePlanID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public decimal LocationLatitude { get; set; }
        public decimal LocationLongitude { get; set; }
        public string LocationFullAddress { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string PlanType { get; set; }
        public decimal ServiceAmount { get; set; }
        public string RequestedServiceDate { get; set; }
        public string ActualServiceDate { get; set; }
        public string ServiceOutDate { get; set; }
        public string Status { get; set; }
    }

    public class ServiceSchedule
    {
        public int ScheduleID { get; set; }
        public int ServiceID { get; set; }
        public string RequestedServiceDate { get; set; }
        public string ActualServiceDate { get; set; }
        public string ServiceOutDate { get; set; }
        public string Status { get; set; }
    }

    public class ServiceRequest
    {
        public int ServicePlanID { get; set; }
        public int AvailableServiceID { get; set; }
        public int VehicleId { get; set; }
        public int RemainderMinutes { get; set; }
        public int LocationID { get; set; }
        public bool IsTeamsandConditionsAccepted { get; set; }
        [DefaultValue(false)]
        public bool? PromoCodeApplied { get; set; }
       // public string PlanType { get; set; }
    }
}
