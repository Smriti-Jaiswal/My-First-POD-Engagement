using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.ViewModel
{
    public class TransactionModel
    {
        public int id { get; set; }
        public decimal amount { get; set; }
        public int userId { get; set; }
    }

    public class TransactionResModel
    {
        public int id { get; set; }
        public decimal amount { get; set; }
        public string type { get; set; }
        public DateTime date { get; set; }
        public string username { get; set; }
        public int madeById { get; set; }
    }

    public class BalanceDetailsModel
    {
        public int UserId { get; set; }
        public int total_debits { get; set; }
        public int total_credits { get; set; }
        public decimal balance { get; set; }
        public string accountNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
