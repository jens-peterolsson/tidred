using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tidred.WebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserPartial()
        {
            return PartialView();
        }

        public ActionResult ProjectPartial()
        {
            return PartialView();
        }

        public ActionResult CustomerPartial()
        {
            return PartialView();
        }

        public ActionResult CustomerEditPartial()
        {
            return PartialView();
        }

    }
}