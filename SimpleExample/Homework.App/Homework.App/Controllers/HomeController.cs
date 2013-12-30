using System.Web.Mvc;

namespace Homework.App.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}