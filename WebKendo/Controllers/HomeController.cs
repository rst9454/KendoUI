using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebKendo.Models;

namespace WebKendo.Controllers
{
    public class HomeController : Controller
    {
        CrudApplicationDbEntities context = new CrudApplicationDbEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetWendorData([DataSourceRequest] DataSourceRequest request)
        {
            var data = context.Wendors.AsQueryable().ToList();
            return Json(data);
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