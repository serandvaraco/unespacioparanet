using CustomDataAnnotation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomDataAnnotation.Controllers
{
    public class VotationController : Controller
    {
        // GET: Votation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VotationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View("Index");
        }



    }
}