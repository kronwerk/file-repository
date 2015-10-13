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
            int max = 3; int min = 0;
            Models.FileModel fM = new Models.FileModel();
            if (id.CompareTo(min) < 0)
                id = 0;
            else if (id.CompareTo(max) > 0)
                id = max;
            if (id.Equals(0))
            {
                fM.content = "Ни одной сказки, песни, басни он не знал и наполовину. Куда?! Котам нельзя, с котами нельзя!!!";
                fM.type = "text";
            } else if (id.Equals(1))
            {
                fM.content = "http://aramgurum.ru/w/15/silovoy_ekzoskelet_crysis_1920x1200.jpg";
                fM.type = "img";
            } else if (id.Equals(2))
            {
                fM.content = "http://www.lifanovsky.com/mp3/zemfira/zemfira_maechki.mp3";
                fM.type = "audio";
            } else if (id.Equals(3))
            {
                fM.content = "http://www.osborneparkbc.com.au/WMV/vendoTrotadora.wmv";
                fM.type = "video";
            }
            fM.name = String.Format("{0} {1}", fM.type, id);
            ViewBag.id = id;
            return View(fM);
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