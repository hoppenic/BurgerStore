using BurgerStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;




namespace BurgerStore.Controllers
{
    public class AccountController : Controller   
    
    {



        SignInManager<BurgerStoreUser> _signInManager;


        //using microsoft.aspnetcore.identity
        public AccountController(SignInManager<BurgerStoreUser> signInManager)
        {

            this._signInManager = signInManager;

        }

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
        [ValidateAntiForgeryToken] //this prevents automated scripts from trying to register
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BurgerStoreUser newEmail = new BurgerStoreUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber
                };
                
                IdentityResult creationResult= this._signInManager.UserManager.CreateAsync(newEmail).Result;

                if (creationResult.Succeeded)
                {
                 IdentityResult passwordResult = this._signInManager.UserManager.AddPasswordAsync(newEmail, model.Password).Result;
                    if (passwordResult.Succeeded)
                    {
                        this._signInManager.SignInAsync(newEmail, false).Wait();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        this._signInManager.UserManager.DeleteAsync(newEmail).Wait();
                        foreach (var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in creationResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

            }

            return View();
        }

        


        public IActionResult SignOut()
        {
            this._signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");

        }

        public IActionResult SignIn()
        {

            return View();

        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(SignInViewModel Model)
        {
            if (ModelState.IsValid)
            {

                BurgerStoreUser existingUser = this._signInManager.UserManager.FindByNameAsync(Model.Email).Result;
                
                if (existingUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult passwordResult = this._signInManager.CheckPasswordSignInAsync(existingUser, Model.Password, false).Result;
                    if (passwordResult.Succeeded)
                    {
                        this._signInManager.SignInAsync(existingUser, false).Wait();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("PasswordIncorrect", "Username or Password is incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserDoesNotExist", "Username or Password is incorrect.");

                }
            }


            return View();

        }


    }
    }




