using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BurgerStore.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        //Responds on GET /account/register
        public IActionResult Register()
        {
            return View();
        }

        //Responds on POST /account/register
        [HttpPost]
        [ValidateAntiForgeryToken] //this prevents automated scripts from trying to login
        public IActionResult Register(Models.RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Create account to log user in
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}