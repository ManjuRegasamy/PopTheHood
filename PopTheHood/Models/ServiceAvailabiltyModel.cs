using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string VehicleImageURL { get; set; }
       // public string ImageType { get; set; }
    }


    public class ServicesModel
    {
        public int AvailableServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public bool IsUserCheckApplicable { get; set; }
        public string BusinessCondition { get; set; }
        public string Notes { get; set; }
        //[DefaultValue(false)]
        //public bool? IsDeleted { get; set; }
    }

    public class ServicePlan
    {
        public int ServicePlanID { get; set; }
        public string PlanType { get; set; }
        public bool IsOfferPlan { get; set; }
    }

    public class VehicleDetailsList
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string VehicleImageURL { get; set; }
        public string LicensePlate { get; set; }
        public int LocationID { get; set; }
        //public decimal LocationLatitude { get; set; }
        //public decimal LocationLongitude { get; set; }
        public string LocationFullAddress { get; set; }
        public int ServicePlanID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int ServicePriceChartId { get; set; }
        public int RemainderMinutes { get; set; }
        public bool IsTeamsandConditionsAccepted { get; set; }
        public string PlanType { get; set; }
        public decimal ServiceAmount { get; set; }
        public int ScheduleID { get; set; }
        public string RequestedServiceDate { get; set; }
        public string ActualServiceDate { get; set; }
        public string ServiceOutDate { get; set; }
        public string Status { get; set; }
        public int PaymentDetailId { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public decimal Promocode_ReducedAmount { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}
