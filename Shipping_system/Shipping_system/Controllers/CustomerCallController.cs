using Newtonsoft.Json;
using Shipping_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shipping_system.DBContext;
using DAL;

namespace Shipping_system.Controllers
{
    public class CustomerCallController : Controller
    {
        // GET: CustomerCall
        public ActionResult Index()
        {
            return View();
        }

        //только с using        
        //DAL.shipping_systemEntities db = new shipping_systemEntities();

        public JsonResult GetCustomerCalls(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            Class1 callsManager = new Class1();
            List<calls> calls = callsManager.GetCalls();


            var CCResults = calls.Select(
                a => new
                {
                    id = a.Id,
                    cell = new object[] {
                  //  a.Id,
                    a.status,
                    a.manager,
                    a.date.ToShortDateString(),
                    a.date_delivery.ToShortDateString(),
                    a.delivery_from,
                    a.delivery_to,
                    a.delivery_time_from.ToString(),
                    a.delivery_time_to.ToString()
                    }
                });
            int totalRecords = CCResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                CCResults = CCResults.OrderByDescending(s => s.id);
                CCResults = CCResults.Skip(pageIndex * pageSize).Take(pageSize);

            }
            else
            {
                CCResults = CCResults.OrderBy(s => s.id);
                CCResults = CCResults.Skip(pageIndex * pageSize).Take(pageSize);

            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = CCResults

            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public string Create([Bind(Exclude = "Id")] calls obj)
        //{
        //    string msg;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.calls.Add(obj);
        //            db.SaveChanges();
        //            msg = "Usyo ok";
        //        }
        //        else
        //        {
        //            msg = "Ne ok";
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        msg = "Err" + ex.Message;
        //    }
        //    return msg;
        //}

        //public string Edit(calls obj)
        //{
        //    string msg;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            msg = "Usyo ok";
        //        }
        //        else
        //        {
        //            msg = "Ne ok";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "Err" + ex.Message;
        //    }
        //    return msg;
        //}

        //public string Delete (int Id)
        //{
        //    calls obj = db.calls.Find(Id);
        //    db.calls.Remove(obj);
        //    db.SaveChanges();
        //    return "Usyo ok";
        //}

        //public string GetRecords()
        //{
        //    List<CustomerCallModel> CCs = new List<CustomerCallModel>();
        //    CCs.Add(new CustomerCallModel()
        //    {
        //        Id = 23,
        //        Status = 1,
        //        Manager = 1,
        //        Date = DateTime.Today,
        //        DateDelivery = DateTime.Today,
        //        DeliveryFrom = "Lviv",
        //        DeliveryTo = "Kyiv",
        //        DeliveryTimeFrom = DateTime.Now,
        //        DeliveryTimeTo = DateTime.Now
        //    });
        //    CCs.Add(new CustomerCallModel()
        //    {
        //        Id = 24,
        //        Status = 1,
        //        Manager = 1,
        //        Date = DateTime.Today,
        //        DateDelivery = DateTime.Today,
        //        DeliveryFrom = "Kharkiv",
        //        DeliveryTo = "Lviv",
        //        DeliveryTimeFrom = DateTime.Now,
        //        DeliveryTimeTo = DateTime.Now
        //    });

        //    return JsonConvert.SerializeObject(CCs);
        //}
    }
}