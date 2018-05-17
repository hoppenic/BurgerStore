using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BurgerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly BurgerStoreDbContext _burgerStoreDbContext;
        //private List<Product> _products;

        public ProductController(BurgerStoreDbContext burgerStoreDbContext)   //injection
        {
            _burgerStoreDbContext = burgerStoreDbContext;
            

        }

        [HttpPost]
        public IActionResult Details(int id, int quantity=1)
        {
            Guid cartId;
            Cart cart = null;
            if (Request.Cookies.ContainsKey("cartId"))
            {
                if(Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = _burgerStoreDbContext.Carts.Include(carts => carts.CartItems).ThenInclude(cartitems => cartitems.Product).FirstOrDefault(x => x.CookieIdentifier == cartId);
                }
            }

            if (cart==null)
            {
                cart = new Cart();
                cartId = Guid.NewGuid();
                cart.CookieIdentifier = cartId;

                _burgerStoreDbContext.Carts.Add(cart);
                Response.Cookies.Append("cartId", cartId.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.UtcNow.AddYears(100) });

            }
            CartItem item = cart.CartItems.FirstOrDefault(x => x.Product.ID == 2);
            if (item == null)
            {
                item = new CartItem();
                item.Product = _burgerStoreDbContext.Products.Find(id);
                cart.CartItems.Add(item);
            }

            item.Quantity+=quantity;
            cart.LastModified = DateTime.Now;

            _burgerStoreDbContext.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Index()
        {
            List<Product> products = _burgerStoreDbContext.Products.ToList();
            return View(products);
        }

        //This is the details method connecting to details view
        public IActionResult Details(int? ID)
        {
            if (ID.HasValue)
            {
                Product p = _burgerStoreDbContext.Products.Find(ID.Value);
                return View(p);
            }
            return NotFound();
        }




    }
}