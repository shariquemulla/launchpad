using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Boilerplate.Models;

namespace Boilerplate.Controllers {

    public class AdminController : Controller {

        private LinkManager linkManager;

        public AdminController(LinkManager linkManager) {
            this.linkManager = linkManager;
        }

        public IActionResult Index() {
            // Check user is logged in
            if( HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Index", "Login");
            }
            return View(linkManager);            
        }

        public IActionResult UpdateCategory(int categoryId) {
            // Check user is logged in
            if( HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Index", "Login");
            }
            Console.WriteLine(categoryId);
            Category category = linkManager.getCategoryByCategoryId(categoryId);
            return View(category);
        }

        [HttpPost]
        public IActionResult UpdateCategorySubmit(Category category) {
            // Check user is logged in
            if( HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Index", "Login");
            }

            if (!ModelState.IsValid) 
                return View("UpdateCategory", category);

            linkManager.Update(category);
            linkManager.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult AddLink(int categoryId) {
            // Check user is logged in
            if( HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Index", "Login");
            }

            Link link = new Link();
            link.categoryId = categoryId;
            ViewData["categoryName"] = linkManager.getCategoryByCategoryId(categoryId).categoryName;
            return View(link);
        }

        [HttpPost]
        public IActionResult AddLinkSubmit(Link link, string pinCheckbox) {
            // Check user is logged in
            if( HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Index", "Login");
            }
            if (!ModelState.IsValid) return RedirectToAction("index");

            link.pinned = pinCheckbox == "pinned" ? true : false;

            linkManager.Add(link);
            linkManager.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult DeleteLink(int linkId) {
            // Check user is logged in
            if( HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Index", "Login");
            }

            Link link = linkManager.getLinkByLinkId(linkId);
            return View(link);
        }

        [HttpPost]
        public IActionResult DeleteLinkSubmit(Link link) {
            // Check user is logged in
            if( HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Index", "Login");
            }

            linkManager.Remove(link);
            linkManager.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult UpdateLink(int linkId) {
            // Check user is logged in
            if( HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.selectList = linkManager.getSelectList();
            
            Link link = linkManager.getLinkByLinkId(linkId);
            return View(link);
        }

        [HttpPost]
        public IActionResult UpdateLinkSubmit(Link link, string pinCheckbox) {
            // Check user is logged in
            if( HttpContext.Session.GetString("auth") != "true") {
                return RedirectToAction("Index", "Login");
            }
            
            if (!ModelState.IsValid) 
                return View("UpdateLink", link);

            link.pinned = pinCheckbox == "pinned" ? true : false;
            linkManager.Update(link);
            linkManager.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
