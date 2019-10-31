using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PopTheHood.Models
{
    public class UsersLogin
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SourceofReg { get; set; }
        //public bool? IsEmailVerified { get; set; } = false;
        //public bool? IsPhoneNumVerified { get; set; } = false;
        [DefaultValue(false)]
        public bool? IsEmailVerified { get; set; }
        [DefaultValue(false)]
        public bool? IsPhoneNumVerified { get; set; }
        public bool? IsPromoCodeApplicable { get; set; }
        public string CreatedDate { get; set; }
        public string Role { get; set; }
        [DefaultValue(0)]
        public int VehicleCount { get; set; }
        //public bool? IsDeleted { get; set; }

    }

    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }


    public class Verification
    {
        public int UserId { get; set; }
        public bool Status { get; set; } = false;
        //[Required]
        //[EnumDataType(typeof(RegSource))]
        //public RegSource RegSource { get; set; }
    }

    public enum RegSource
    {
        Phone,
        Email
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int LocationID { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationFullAddress { get; set; }
    }
}


