using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index(string id)
        {
            return View();
        }
    }








    }
}