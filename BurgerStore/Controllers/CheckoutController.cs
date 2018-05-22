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
        private  BurgerStoreDbContext _burgerStoreDbContext;
        private EmailService _emailService;

        public CheckoutController(BurgerStoreDbContext burgerstoredbcontext, EmailService emailService)
        {

            this._burgerStoreDbContext = burgerstoredbcontext;
            this._emailService = emailService;

        }


        // GET: /Checkout
        public async Task<IActionResult> Index()
        {

            CheckoutViewModel model = new CheckoutViewModel();
            Guid cartId;
            Cart cart = null;

            if (Request.Cookies.ContainsKey("cartId"))
            {
                if(Guid.TryParse(Request.Cookies["cartId"],out cartId))
                {
                    cart = await _burgerStoreDbContext.Carts
                        .Include(carts => carts.CartItems)
                        //.ThenInclude(cartitems.Product)
                        .FirstOrDefaultAsync(x => x.CookieIdentifier == cartId);
                }
            }
            if (cart == null)
            {
                return RedirectToAction("Index", "Home");
            }

            model.Cart = cart;

            return View(model);
        }



        //HTTPPost
        public IActionResult Submit()
        {

            return View();

        }






    }



}
