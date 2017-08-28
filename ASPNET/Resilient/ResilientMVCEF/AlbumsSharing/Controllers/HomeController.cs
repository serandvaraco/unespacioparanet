using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlbumsSharing.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [ValidateInput(false)]
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg; 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string msg) {

            return View("Index"); 
        }
    }
}