using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BurgerStore.Models
{
    public class CheckoutViewModel
    {

        public Cart Cart { get; set; }

        [Required]
        [Display(Name ="Email")]
        public string ContactEmail { get; set; }

        [Required]
        [Display(Name ="Phone Number")]
        public string ContactPhoneNumber { get; set; }
  
        [Required]
        [Display(Name ="Address")]
        public string ShippingAddress { get; set; }

        [Required]
        [Display(Name ="City")]
        public string ShippingLocale { get; set; }

        [Required]
        [Display(Name ="State")]
        public string ShippingRegion { get; set; }

        [Required]
        [Display(Name ="Country")]
        public string ShippingCountry { get; set; }

        [Required]
        [Display(Name ="Zip Code")]
        public string ShippingPostalCode { get; set; }

        [Required]
        [Display(Name ="Name on card")]    
        public string NameOnCard { get; set; }

        [Required]
        [Display(Name ="Credit Card Number")]
        [MaxLength(16)]
        public string BillingCardNumber { get; set; }

        [Required]
        [Display(Name ="Expiration Month")]
        [Range(1,12)]
        public string BillingCardExpirationMonth { get; set; }

        [Required]
        [Display(Name ="Expiration Year")]
        public string BillingCardExpirationYear { get; set; }

        [Required]
        [Display(Name ="CVV")]
        public string BillingCardVerificationValue { get; set; }





    }
}
