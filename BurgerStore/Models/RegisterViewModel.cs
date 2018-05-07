using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerStore.Models
{
    public class RegisterViewModel
    {

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MinLength(5, ErrorMessage = "You need to make your username at least 5 letters.")]
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string Email { get; set; }

   

        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }



    }
}
