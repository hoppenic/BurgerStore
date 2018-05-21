using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurgerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly BurgerStoreDbContext _burgerStoreDbContext;

        public CheckoutController(BurgerStoreDbContext burgerstoredbcontext)
        {
            _burgerStoreDbContext = burgerstoredbcontext;
        }


        // GET: /Checkout
        public IActionResult Index()
        {
            
            return View();

        }



        //HTTPPost
        public IActionResult Submit()
        {

            return View();

        }






    }



}
