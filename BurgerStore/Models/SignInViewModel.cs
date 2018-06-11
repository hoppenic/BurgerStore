using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BurgerStore.Models
{
    public class SignInViewModel
    {

        [Required]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "Password must be at least 8 characters in length.")]
        [Required]      
        public string Password { get; set; }


    }
}
