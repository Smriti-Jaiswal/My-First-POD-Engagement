using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entity.Models
{
    public class Service
    {
        public int? ServiceId { get; set; }
        public int ReqBy { get; set; }
        public string ReqWhat { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? IsApproved { get; set; }
        public decimal? Amount { get; set; }
    }
}
