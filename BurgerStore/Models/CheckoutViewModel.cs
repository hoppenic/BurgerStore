using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BurgerStore.Models
{
    public class CheckoutViewModel
    {
        //payment stuff

        [Required(ErrorMessage ="This is a required field.")]
        public double ccNumber { get; set; }

        [Required(ErrorMessage ="This is a required field.")]
        public int ccVerify { get; set; }

        [Required(ErrorMessage ="This is a required field.")]
        public string nameOnCard { get; set; }


        //personal info stuff

        [Required]
        public string billAddress { get; set; }

        [MinLength(2,ErrorMessage ="Must be at least two characters.")]
        [Required]
        public string state { get; set; }

        [Required(ErrorMessage ="This is a required field.")]
        public string city { get; set; }

        [MinLength(10,ErrorMessage ="Must be at least ten characters.")]
        [Required]
        public int phoneNumber { get; set; }

      



    }
}
