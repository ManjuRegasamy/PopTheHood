using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopTheHood.Models
{
    public class PromoCodeModel
    {
        //public int PromocodeID { get; set; }
        public string PromocodeText { get; set; }
        public string PromoType { get; set; }
        public decimal PromocodeValue { get; set; }
        public int NumberofUsePerUser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }


    public class PromoCodes
    {
        public string PromocodeText { get; set; }
        public string UserId { get; set; }
    }

    public class PromoCodeList
    {
        public int PromocodeID { get; set; }
        public string PromocodeText { get; set; }
        public string PromoType { get; set; }
        public decimal PromocodeValue { get; set; }
        //public int NumberofUsePerUser { get; set; }
        public int UserId { get; set; }
        public string StartDate { get; set; }
        public string ExpiryDate { get; set; }
    }

}
