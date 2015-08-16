using System.Web.Mvc;

namespace TodoApp.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}