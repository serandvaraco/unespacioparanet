using FacRepositoriesContainer.Models;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FacRepositoriesContainer.Controllers
{
    public class IndexController : Controller
    {
        IuowRep _uowRep;
        public IndexController(IuowRep uowRep)
        {
            _uowRep = uowRep;
        }

        [Route("GetProjects")]
        [HttpPost]
        public JsonResult Index()
        {
            IEnumerable<dynamic> projects = new[] {

                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),
                ((IRepository)_uowRep.Project).GetAll(),

            };
            return Json(projects);
        }
    }
}