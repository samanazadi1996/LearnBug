using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LoginUserViewModel
    {

        [Required(ErrorMessage ="نام کاربری خود را وارد کنید")]
        public string Username { get; set; }

        [Required(ErrorMessage = "رمز عبور خود را وارد کنید")]
        public string Password { get; set; }

        public bool Rememberme { get; set; }

    }
}
