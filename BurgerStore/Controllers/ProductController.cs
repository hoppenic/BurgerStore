using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurgerStore.Models;


namespace BurgerStore.Controllers
{
    public class ProductController : Controller
    {
        private List<Product> _products;

        public ProductController()
        {
            _products = new List<Product>();
            _products.Add(new Product
            {
                ID = 1,
                Name = " Traditional Ground Beef Burger",
                Description = "Ground Beef",
                Image =" ",
                Price = 5.99m,
                Organic=false

            });
            _products.Add(new Product
            {
                ID = 2,
                Name = " Traditional Ground Turkey Burger",
                Description = "Ground Turkey",
                Image = " ",
                Price = 4.99m,
                Organic = false

            });
        }


        


        public IActionResult Index()
        {
            return View(_products);
        }

        //This is the details method connecting to details view
        public IActionResult Details(int? ID)
        {
            if (ID.HasValue)
            {
                Product p = _products.Single(x => x.ID == ID.Value);
                return View(p);
            }
            return NotFound();
        }




    }
}