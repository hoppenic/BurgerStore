using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerStore.Models
{
    public class SignInViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.MinLength(8, ErrorMessage = "Password must be at least 8 characters in length.")]
        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }


    }
}
