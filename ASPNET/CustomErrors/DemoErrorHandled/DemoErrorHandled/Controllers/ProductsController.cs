using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoErrorHandled.Controllers
{
    public class ProductsController : Controller
    {

        [HandleError(ExceptionType = typeof(NotImplementedException), View = "NotImplement")]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        public ActionResult NotImplement() {

            return View(); 
        }
    }
}