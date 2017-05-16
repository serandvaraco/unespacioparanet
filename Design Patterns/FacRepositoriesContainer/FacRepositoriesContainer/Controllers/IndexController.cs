using FacRepositoriesContainer.Models;
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
            return Json(((IRepository)_uowRep.Project).GetAll());
        }
    }
}