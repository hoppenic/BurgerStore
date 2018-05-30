using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BurgerStore.Models
{
    public class RegisterViewModel
    {
        [EmailAddress]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MinLength(5, ErrorMessage = "User name must be at least 5 characters.")]
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage ="Password must be at least 8 characters.")]
        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        
        [Required]
        public string FirstName { get; set; }


        [Required]
        public string LastName { get; set; }



    }
}
