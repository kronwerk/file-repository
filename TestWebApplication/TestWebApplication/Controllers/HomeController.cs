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

        public ActionResult FilePage(int id = 0)
        {
            string type = "text";
            int max = 2; int min = 0; 
            if (id.CompareTo(min) < 0)
                id = 0;
            else if (id.CompareTo(max) > 0)
                id = max;
            if (id.Equals(0))
            {
                ViewBag.Content = "Ни одной сказки, песни, басни он не знал и наполовину. Куда?! Котам нельзя, с котами нельзя!!!";
            } else if (id.Equals(1))
            {
                type = "img";
            } else if (id.Equals(2))
            {
                type = "audio";
            };
            ViewBag.ID = id;
            ViewBag.Name = String.Format("{0} {1}", type, id);
            ViewBag.Type = type;
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript",
            //    "~/Content/content_chooser.js");
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