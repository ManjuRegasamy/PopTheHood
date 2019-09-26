using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Models
{
    public class PaymentModel
    {
        
    }

    public class VehiclePaymentDetails
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string PlanType { get; set; }
        public decimal ServiceAmount { get; set; }
        public string RequestedServiceDate { get; set; }
        public string ActualServiceDate { get; set; }
        public string ServiceOutDate { get; set; }
        public string Status { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string Promocode_ReducedAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentDate { get; set; }

    }

}
