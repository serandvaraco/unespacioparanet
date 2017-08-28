using FacRepositoriesContainer.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacRepositoriesContainer.Controllers
{

    public class AlbumsController : Controller
    {
        IUoWModel uow;
        public AlbumsController(IUoWModel uow)
        {
            this.uow = uow;
        }

        public ActionResult Index()
        {
            return View("Index", uow.Albums.GetQueryEntity().ToList());
        }

     
        public ActionResult GetAlbums(int id)
        {
            return Json(uow.Albums.GetById(id),
                JsonRequestBehavior.AllowGet);
        }
    }
}