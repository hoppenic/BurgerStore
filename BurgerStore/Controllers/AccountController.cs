using BurgerStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using Braintree;


namespace BurgerStore.Controllers
{
    public class AccountController : Controller   
    {
       
        SignInManager<BurgerStoreUser> _signInManager;       
        EmailService _emailService;
        Braintree.BraintreeGateway _braintreeGateway;

        //using microsoft.aspnetcore.identity
        public AccountController(SignInManager<BurgerStoreUser> signInManager, EmailService emailService, Braintree.BraintreeGateway braintreeGateway)
        {
            this._signInManager = signInManager;
            this._emailService = emailService;
            this._braintreeGateway = braintreeGateway;
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            //Check to confirm that my register model is filled out correctly
            if (ModelState.IsValid)
            {

                //this is creating my new user.  I simply used email only rather than username
                BurgerStoreUser newEmail = new BurgerStoreUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber
                };
                
                IdentityResult creationResult= await this._signInManager.UserManager.CreateAsync(newEmail);

                if (creationResult.Succeeded)
                {
               

                 IdentityResult passwordResult = await this._signInManager.UserManager.AddPasswordAsync(newEmail, model.Password);
                    if (passwordResult.Succeeded)
                    {

                        Braintree.CustomerSearchRequest search = new Braintree.CustomerSearchRequest();
                        search.Email.Is(model.Email);
                        var searchResult = await _braintreeGateway.Customer.SearchAsync(search);
                        if(searchResult.Ids.Count == 0)
                        {
                            //creating a new braintree customer here
                            await _braintreeGateway.Customer.CreateAsync(new Braintree.CustomerRequest
                            {
                                Email = model.Email,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Phone = model.PhoneNumber


                            });
                        }
                        else
                        {
                            //update the existing braintree customer

                            Braintree.Customer existingCustomer = searchResult.FirstItem;
                            await _braintreeGateway.Customer.UpdateAsync(existingCustomer.Id, new Braintree.CustomerRequest
                            {
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Phone = model.PhoneNumber


                            });


                        }
                            


                        var confirmationToken = await _signInManager.UserManager.GenerateEmailConfirmationTokenAsync(newEmail);

                        confirmationToken = System.Net.WebUtility.UrlEncode(confirmationToken);

                        string currentUrl = Request.GetDisplayUrl();
                        System.Uri uri = new System.Uri(currentUrl);
                        string confirmationUrl = uri.GetLeftPart(System.UriPartial.Authority);
                        confirmationUrl += "/account/confirm?id=" + confirmationToken + "&userId=" + System.Net.WebUtility.UrlEncode(newEmail.Id);
                        await this._signInManager.SignInAsync(newEmail, false);

                       var emailResult = await this._emailService.SendEmailAsync(model.Email, "Welcome to Flavor Town Burgers", "<p> Thanks for signing up, " + model.Email + "!</p><p>< a href =\"" + confirmationUrl + "\">Confirm your account<a></p>","Thanks for signing up, " + model.Email);
                        if (emailResult.Success)
                            return RedirectToAction("Index", "Home");
                        else
                            return BadRequest(emailResult.Message);
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

        


        public async Task<IActionResult> SignOut()
        {
           await this._signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel Model)
        {
            if (ModelState.IsValid)
            {

                BurgerStoreUser existingUser = await this._signInManager.UserManager.FindByNameAsync(Model.Email);
                
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

        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {

            if((ModelState.IsValid) && (!string.IsNullOrEmpty(email)))
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(email);
                if(user != null)
                {
                    var resetToken = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(user);

                    resetToken = System.Net.WebUtility.UrlEncode(resetToken);
                    string currentUrl = Request.GetDisplayUrl(); //gets url for current request
                    System.Uri uri = new Uri(currentUrl); //wraps it in a 'uri' object so I can split it into parts
                    string resetUrl = uri.GetLeftPart(UriPartial.Authority); //gives me the scheme + authority of the URL
                    resetUrl += "/account/resetpassword?id=" + resetToken + "&userId=" + System.Net.WebUtility.UrlEncode(user.Id);


                    string htmlContent= "<a href=\"" + resetUrl + "\">Reset your password</a>";
                    var emailResult = await _emailService.SendEmailAsync(email, "Reset your password", htmlContent, resetUrl);
                    if (emailResult.Success)
                    {
                        return RedirectToAction("ResetSent");
                    }
                    else
                    {
                        return BadRequest(emailResult.Message);
                    }
                }
            }
            ModelState.AddModelError("email", "Email is not valid");
            return View();
        }

        public IActionResult ResetSent()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string id,string userId,string password)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(userId);
            if(user != null)
            {
                await _signInManager.UserManager.ResetPasswordAsync(user, id, password);
                return RedirectToAction("SignIn");
            }
            return BadRequest();
        }

        public async Task<IActionResult> Confirm(string id, string userId)
        {
            var user = await _signInManager.UserManager.FindByIdAsync(userId);
            if(user != null)
            {
                await _signInManager.UserManager.ConfirmEmailAsync(user, id);
                return RedirectToAction("Index", "Home");
            }
            return BadRequest();
        }
       
        
    }
}




