using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BurgerStore.Models
{
    public class Product
    {
        public int ID { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name="Price/lb")]
        public decimal? Price { get; set; }

        public string Image { get; set; }

       
        public bool Organic { get; set; }

       
        public bool Grassfed { get; set; }




    }
}
