using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurgerStore.Models;

namespace BurgerStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly BurgerStoreDbContext _burgerStoreDbContext;

        public CheckoutController(BurgerStoreDbContext burgerstoredbcontext)
        {
            _burgerStoreDbContext = burgerstoredbcontext;
        }


        // GET: ProductsAdmin/Create
        public IActionResult Index()
        {
            return View();
        }

        //HTTPPost
        public IActionResult Checkout2()
        {

            return View();
            
        }


      
    }
}