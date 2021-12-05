using System;
using Microsoft.AspNetCore.Mvc;
using Boilerplate.Models;

namespace Boilerplate.Controllers {

    public class PublicController : Controller {

        private LinkManager linkManager;

        public PublicController(LinkManager linkManager) {
            this.linkManager = linkManager;
        }

        public IActionResult Index() {
            return View(linkManager);
        }

    }
}
