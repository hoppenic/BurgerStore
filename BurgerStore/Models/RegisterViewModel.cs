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
        [MinLength(10,ErrorMessage ="Must be at least ten numbers")]
        [MaxLength(10,ErrorMessage ="Must be no more than ten numbers")]
        public string PhoneNumber { get; set; }

        
        [Required]
        [MinLength(2,ErrorMessage ="Must be no less than two characters.")]
        public string FirstName { get; set; }


        [Required]
        [MinLength(2,ErrorMessage ="Must be no less than two characters")]
        public string LastName { get; set; }



    }
}
