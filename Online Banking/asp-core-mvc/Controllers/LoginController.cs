using Microsoft.AspNetCore.Mvc;
using asp_core_mvc.Models;
using Microsoft.AspNetCore.Http;

namespace asp_core_mvc.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if (DatabaseHandler.validateUser(login))
            {
                HttpContext.Session.SetString("Name", login.FullName);
                HttpContext.Session.SetInt32("CustomerID", login.CustomerID);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Incorrect Username/Password");
                return View("Index");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}