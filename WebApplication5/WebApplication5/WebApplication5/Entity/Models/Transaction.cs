using WebApplication5.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entity.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? IsReqTransaction { get; set; }

        public int MadeBy { get; set; }
        public virtual User User { get; set; }
    }
}
