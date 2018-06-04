using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BurgerStore.Models;
using Microsoft.EntityFrameworkCore;
using Braintree;

namespace BurgerStore.Controllers
{
    public class CheckoutController : Controller
    {
        private BurgerStoreDbContext _burgerStoreDbContext;
        private EmailService _emailService;
        private SignInManager<BurgerStoreUser> _signInManager;
        private BraintreeGateway _braintreeGateway;
        private SmartyStreets.USStreetApi.Client _usStreetApiClient;


        public CheckoutController(BurgerStoreDbContext burgerstoredbcontext, EmailService emailService, SignInManager<BurgerStoreUser> signInManager, BraintreeGateway braintreeGateway, SmartyStreets.USStreetApi.Client usStreetApiClient)
        {

            this._burgerStoreDbContext = burgerstoredbcontext;
            this._emailService = emailService;
            this._signInManager = signInManager;
            this._braintreeGateway = braintreeGateway;
            this._usStreetApiClient = usStreetApiClient;

        }


        // GET: /Checkout
        public async Task<IActionResult> Index()
        {

            CheckoutViewModel model = new CheckoutViewModel();
            await GetCurrentCart(model);
            if (model.Cart == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);

        }

        private async Task GetCurrentCart(CheckoutViewModel model)
        {
            Guid cartId;
            Cart cart = null;

            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _signInManager.UserManager.GetUserAsync(User);
                model.ContactEmail = currentUser.Email;
                model.ContactPhoneNumber = currentUser.PhoneNumber;
            }

            if (Request.Cookies.ContainsKey("cartId"))
            {
                if (Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = await _burgerStoreDbContext.Carts
                        .Include(carts => carts.CartItems)
                        .ThenInclude(cartitems => cartitems.Product)
                        .FirstOrDefaultAsync(x => x.CookieIdentifier == cartId);

                }
            }

            model.Cart = cart;


        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel model)
        {
            await GetCurrentCart(model);

            if (ModelState.IsValid)
            {
                
               
                Order newOrder = new Order
                {
                    TrackingNumber = Guid.NewGuid().ToString(),
                    OrderDate = DateTime.Now,
                    OrderItems = model.Cart.CartItems.Select(x => new OrderItem
                    {
                        ProductID = x.Product.ID,
                        ProductName = x.Product.Name,
                        ProductPrice = (x.Product.Price ?? 0),
                        Quantity = x.Quantity
                    }).ToArray(),
                    ShippingAddress = model.ShippingAddress,
                    ShippingCountry = model.ShippingCountry,
                    ContactEmail = model.ContactEmail,
                    ContactPhoneNumber = model.ContactPhoneNumber,
                    ShippingLocale = model.ShippingLocale,
                    ShippingPostalCode = model.ShippingPostalCode,
                    ShippingRegion = model.ShippingRegion,

                };


                //this is our braintree transaction section

                TransactionRequest transaction = new TransactionRequest
                {
                    //Amount = 1,
                    Amount = model.Cart.CartItems.Sum(x => x.Quantity * (x.Product.Price ?? 0)),

                    CreditCard = new TransactionCreditCardRequest
                    {
                        Number = model.BillingCardNumber,
                        CardholderName = model.NameOnCard,
                        CVV = model.BillingCardVerificationValue,
                        ExpirationMonth = model.BillingCardExpirationMonth.ToString().PadLeft(2, '0'),
                        ExpirationYear = model.BillingCardExpirationYear.ToString()
                    }

                };

                var transactionResult = await _braintreeGateway.Transaction.SaleAsync(transaction);
                if (transactionResult.IsSuccess())
                {



                    _burgerStoreDbContext.Orders.Add(newOrder);
                    _burgerStoreDbContext.CartItems.RemoveRange(model.Cart.CartItems);
                    _burgerStoreDbContext.Carts.Remove(model.Cart);
                    await _burgerStoreDbContext.SaveChangesAsync();

                    //Try to checkout
                    Response.Cookies.Delete("cartId");
                    return RedirectToAction("Index", "Receipt", new { id = newOrder.TrackingNumber });
                }
                for (int i = 0; i < transactionResult.Errors.Count; i++)
                {
                    ModelState.AddModelError("BillingCardNumber" + i, transactionResult.Errors.All()[i].Message);
                }



            }
            return View(model);
        }
    }

}








        



    