using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Models
{
    public class VehiclesModel
    {
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string SpecialNotes { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public byte[] VehicleImage { get; set; }
        public string ImageType { get; set; }
    }

   public class UserVehicleDetails
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string SourceofReg { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneNumVerified { get; set; }
        public bool IsPromoCodeApplicable { get; set; }
        public string UserCreatedDate { get; set; }
        public int VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string SpecialNotes { get; set; }
        public string VehicleCreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] VehicleImage { get; set; }
        public string ImageType { get; set; }
    }

    public class VehiclesDetails
    {
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public string SpecialNotes { get; set; }
        public byte[] VehicleImage { get; set; }
        public string ImageType { get; set; }
        //public string VehicleCreatedDate { get; set; }
    }


    public class VehicleLocation
    {
        public int LocationID { get; set; }
        public int VehicleId { get; set; }
        public decimal LocationLatitude { get; set; }
        public decimal LocationLongitude { get; set; }
        public string LocationFullAddress { get; set; }
        public string LandMark { get; set; }
    }
}


