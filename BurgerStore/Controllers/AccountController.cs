using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BurgerStore.Models;
using System.Threading.Tasks;

namespace BurgerStore.Controllers
{
    public class AccountController : Controller   
    
    {

        SignInManager<IdentityUser> _signInManager;


        //using microsoft.aspnetcore.identity
        public AccountController(SignInManager<IdentityUser> signInManager)
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
                IdentityUser newEmail = new IdentityUser(model.Email);
                newEmail.Email = model.Email;
                IdentityResult creationResult = this._signInManager.UserManager.CreateAsync(newEmail).Result;
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

        //Weekend Challenge here
        //Create the Views and any additional Models required for the functionality below
        //Check the methods on UserManager and SignInManager to figure out how to do this!!
        //Beware of online examples!  Things might be renamed in code you read, or it might be DotNetFramework4.6
        //Update your Layout to display the correct links depending on whether the user is logged in / out


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
        public IActionResult SignIn(Models.SignInViewModel Model)
        {
            if (ModelState.IsValid)
            {

                IdentityUser existingUser = this._signInManager.UserManager.FindByNameAsync(model.Email).Result;
                
                if (existingUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult passwordResult = this._signInManager.CheckPasswordSignInAsync(existingUser, model.Password, false).Result;
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




