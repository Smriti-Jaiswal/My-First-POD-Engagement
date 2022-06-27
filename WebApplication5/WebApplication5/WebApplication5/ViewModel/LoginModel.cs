using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.ViewModel
{
    public class LoginReqModel
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }

    public class LoginResModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    }
}
