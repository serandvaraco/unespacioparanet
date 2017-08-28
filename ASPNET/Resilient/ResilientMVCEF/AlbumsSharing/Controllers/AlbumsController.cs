using AlbumsSharing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlbumsSharing.Controllers
{
    public class AlbumsController : Controller
    {

        IUnitOfWork uow;
        public AlbumsController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server,Duration =20)]
        public ActionResult Index()
        {
            return View("Index", uow.Albums.GetQueryEntity());
        }

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Client, Duration = 9000)]
        public ActionResult Create()
        {
            return View(new Model.Entities.Albums());
        }
        [HttpPost]
        public ActionResult Create(Model.Entities.Albums model)
        {
            if (ModelState.IsValid)
            {
                uow.Albums.Add(model);
                uow.Commit();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("Create", "Album invalid");
            return View();
        }


    }
}