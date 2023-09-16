
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using KendoUIApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filter = KendoUIApp1.Extension.Filter;

public class HomeController : Controller
{
    ProductionDBEntities context = new ProductionDBEntities();
    public ActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public ActionResult GetData([DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest request)
    {
        //var data = context.Wendors.ToList();
        //return Json(data);
        //int skip = (request.Page - 1) * request.PageSize;
        //var filter = new Filter();
        //filter.Value = "sunil";
        //filter.Operator = "eq";
        //filter.Logic = "and";
        //var employees = context.Wendors.AsQueryable().ToDataSourceResult(request.PageSize, skip, null, null,  null, null);

        var employees = context.Wendors.OrderBy(e => e.Name).ToDataSourceResult(request);
        return Json(employees, JsonRequestBehavior.AllowGet);

    }
}
