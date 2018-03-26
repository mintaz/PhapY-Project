using System.Web.Mvc;

namespace PhapY.Web.Controllers
{
    public class AboutController : PhapYControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}