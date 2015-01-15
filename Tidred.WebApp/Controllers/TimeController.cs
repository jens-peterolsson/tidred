using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tidred.WebApp.Controllers
{
    [Authorize]
    public class TimeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string Hello()
        {
            return "Well, hello there " + User.Identity.Name;
        }
    }
}
