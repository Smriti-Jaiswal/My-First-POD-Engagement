using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.ViewModel
{
    public class ServiceReqModel
    {
        public string type { get; set; }
        public decimal amount { get; set; }
    }

    public class ServiceApproveModel
    {
        public bool? isApproved { get; set; }
        public int id { get; set; }
    }

    public class ServiceResModel
    {
        public int? id { get; set; }
        public string reqBy { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public DateTime date { get; set; }
    }
}
