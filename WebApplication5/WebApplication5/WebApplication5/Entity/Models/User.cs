using WebApplication5.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entity.Models
{
    public class User
    {
        public User()
        {
            Transactions = new HashSet<Transaction>();
        }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }       
        public int? AccountId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
