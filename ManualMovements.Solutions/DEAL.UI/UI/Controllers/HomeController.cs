using Deal.UI.Models.ManualMovements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace Deal.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int page = 1)
        {
            var vw = new IndexViewModel();
            int pageSize = Convert.ToInt16
                (ConfigurationManager.AppSettings["QuantityRegistrationPage"]);
            int pageNumber = page;

            var data = Services.ManualMovements.Index.List(new Models.ManualMovements.Index
            {
            });
            if (data != null)
                vw.Indexes = data.ToPagedList(pageNumber, pageSize);

            return View(vw);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}