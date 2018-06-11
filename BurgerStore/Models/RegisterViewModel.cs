using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BurgerStore.Models
{
    public class RegisterViewModel
    {

        //model validation

        [EmailAddress]
        [Required]
        [MinLength(5, ErrorMessage = "Must be at least five characters.")]
        [System.ComponentModel.DataAnnotations.MaxLength(50, ErrorMessage ="Must be no more than fifty characters.")]
        public string Email { get; set; }

        [Required]
        //have something in startup that automatically demands at least 8 characters
        public string Password { get; set; }

        [Required]
        [Phone]
        [MinLength(10,ErrorMessage ="Must be at least ten numbers")]
        [MaxLength(10,ErrorMessage ="Must be no more than ten numbers.")]
        public string PhoneNumber { get; set; }

        
        [Required]
        [MinLength(2,ErrorMessage ="Must be no less than two characters.")]
        public string FirstName { get; set; }


        [Required]
        [MinLength(2,ErrorMessage ="Must be no less than two characters.")]
        public string LastName { get; set; }



    }
}
