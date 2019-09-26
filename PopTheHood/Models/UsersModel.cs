﻿using System;
using System.Collections.Generic;
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
        public bool? IsEmailVerified { get; set; }
        public bool? IsPhoneNumVerified { get; set; }
        public bool? IsPromoCodeApplicable { get; set; }
        public string CreatedDate { get; set; }
        //public string ModifiedDate { get; set; }
        //public bool? IsDeleted { get; set; }
    }

    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}


