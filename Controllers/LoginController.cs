using System;
using Microsoft.AspNetCore.Mvc;
using Boilerplate.Models;

namespace userAuthentication.Controllers {

    public class LoginController : Controller {

        public IActionResult Index() {
            return View();
        }

        public IActionResult Login(string myUsername, string myPassword) {
            WebLogin webLogin = new WebLogin(HttpContext);
            webLogin.username = myUsername;
            webLogin.password = myPassword;

            // attempt to login
            if(webLogin.unlock()) {
                return RedirectToAction("Index", "Admin");
            } else {
                ViewData["feedback"] = "Incorrect login. Please try again...";
            }

            return View("Index");
        }

        public IActionResult Logout() {
            WebLogin webLogin = new WebLogin(HttpContext);
            webLogin.logout();
            return View("Index");
        }

    }
}
