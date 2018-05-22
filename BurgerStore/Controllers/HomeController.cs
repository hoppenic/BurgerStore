using BurgerStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BurgerStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
           
            return View();
        }

        public IActionResult Contact()
        {
           
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
