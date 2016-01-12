using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Repo Search Page";

            return View();
        }
        public ActionResult SearchUser()
        {
            ViewBag.Message = "User Search Page";

            return View();
        }
        public ActionResult SearchFile()
        {
            ViewBag.Message = "User Search Page";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}