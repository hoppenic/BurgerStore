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

            //these are lists
            _products.Add(new Product
            {
                ID = 1,
                Name = " Traditional Ground Beef Burger",
                Description = "Ground Beef",
                Image = " ",
                Price = 5.99m,
                Organic = false,
                Grassfed=false
            });

            _products.Add(new Product
            {
                ID = 2,
                Name = "Organic Grassfed Ground Beef Burger",
                Description = "Organic Grassfed Ground Beef",
                Image = " ",
                Price = 7.99m,
                Organic = true,
                Grassfed = true

            });

            _products.Add(new Product
            {
                ID = 3,
                Name = " Ground Turkey Burger",
                Description = "Ground Turkey",
                Image = " ",
                Price = 3.99m,
                Organic = false,
                Grassfed=false

            });

            _products.Add(new Product
            {
                ID = 4,
                Name = " Salmon Burger",
                Description = "Ground Salmon",
                Image = " ",
                Price = 6.99m,
                Organic = false,
                Grassfed=false
            });

            _products.Add(new Product
            {
                ID = 5,
                Name = "Ostrich Burger",
                Description = "Ground Ostrich",
                Image = " ",
                Price = 10.99m,
                Organic = false,
                Grassfed = false

            });

            _products.Add(new Product
            {
                ID = 6,
                Name = " Organic Chicken Burger",
                Description = " Organic Ground Chicken",
                Image = " ",
                Price=4.99m,
                Organic=true,
                Grassfed=false

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