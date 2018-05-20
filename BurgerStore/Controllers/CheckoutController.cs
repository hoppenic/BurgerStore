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


        // GET: ProductsAdmin/Create
        public IActionResult Index()
        {
         
            Guid cartId;
            Cart cart = null; ;
            if (Request.Cookies.ContainsKey("cartId"))
            {
                if(Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = _burgerStoreDbContext.Carts.Include(carts => carts.CartItems).ThenInclude(cartitems => cartitems.Product).FirstOrDefault(x => x.CookieIdentifier == cartId);
                }
            }

           
            return View(cart);

        }



        //HTTPPost
        public IActionResult Checkout()
        {

            return View();

        }






    }



}
