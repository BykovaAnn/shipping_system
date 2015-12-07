using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Managers;
using DAL;
using Newtonsoft.Json;
using Shipping_system.Models;


namespace Shipping_system.Controllers
{
 
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin, manager")]
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

        [HttpGet]
        public ActionResult GetData(Int32 page, Int32 perPage, String sort)
        {
            List<calls> callsForGridb = CallsManager.GetCalls(page, perPage, sort, "desc");
            var result = new {




            };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}