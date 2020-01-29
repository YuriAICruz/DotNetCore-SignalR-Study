using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace WebServerStudy.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var data = 2;
            return View(data);
        }
    }
}