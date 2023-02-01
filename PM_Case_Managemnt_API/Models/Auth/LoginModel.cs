using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM_Case_Managemnt_API.Models.Auth
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
