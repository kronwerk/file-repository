using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FilePage()
        {
            return View();
        }

        public ActionResult PersonalPage()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
    }
}