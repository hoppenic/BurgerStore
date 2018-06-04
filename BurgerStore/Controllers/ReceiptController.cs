using System;
using System.Collections.Generic;
using BurgerStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Braintree;


namespace BurgerStore.Controllers
{
    public class ReceiptController : Controller
    {
     

        SignInManager<BurgerStoreUser> _signInManager;
        EmailService _emailService;
        Braintree.BraintreeGateway _braintreeGateway;

        //using microsoft.aspnetcore.identity
        public ReceiptController(SignInManager<BurgerStoreUser> signInManager, EmailService emailService, Braintree.BraintreeGateway braintreeGateway)
        {
            this._signInManager = signInManager;
            this._emailService = emailService;
            this._braintreeGateway = braintreeGateway;
        }


        //GET: Receipt/Index
        public IActionResult Index()
        {

            //check to see that my CheckoutviewModel is validly filled out
            if (ModelState.IsValid)
            {
                //just show them up mocked up receipt page
              return View();
            }

            else
            {
                //foreach(var error in ....  )
                //this is wrong, I had to just leave it for the meantime.
                return View();

                
            }
        }
    }
}










  