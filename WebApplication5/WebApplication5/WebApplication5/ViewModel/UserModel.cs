using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Options;

namespace WebApplication5.ViewModel
{
    public class UserModel
    {
        public int id { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public DateTime dateOfBirth { get; set; }
        [Required]
        public string address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        public string password { get; set; }
        public string accountNumber { get; set; }
        public decimal openAmount { get; set; }
        public string userType { get; set; }
    }
}
