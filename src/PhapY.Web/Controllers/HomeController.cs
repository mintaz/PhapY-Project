using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace PhapY.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : PhapYControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}