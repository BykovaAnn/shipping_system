using Newtonsoft.Json;
using Shipping_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace Shipping_system.Controllers
{
    public class CustomerCallController : Controller
    {
        // GET: CustomerCall
        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CustomerCallModel newcalldata)
        {
            DAL.Customers.CustomerCalls.AddCall((int)System.Web.Security.Membership.GetUser().ProviderUserKey,
                newcalldata.DeliveryFrom,
                newcalldata.DeliveryTo,
                newcalldata.DateDelivery,
                newcalldata.DeliveryTimeFrom,
                newcalldata.DeliveryTimeTo);
            return View();
        }

        public JsonResult GetCall(Int32 callID)
        {
            DAL.Customers.CustomerCalls CustomerCallManager = new DAL.Customers.CustomerCalls();
            calls call = CustomerCallManager.GetCall(callID);
            CustomerCallModel model = new CustomerCallModel()
            {
                Id = call.Id,
                DeliveryFrom = call.delivery_from,
                DeliveryTo = call.delivery_to,
                DeliveryTimeFrom = call.delivery_time_from.HasValue ? call.delivery_time_from.Value.ToString() : String.Empty,
                DeliveryTimeTo = call.delivery_time_to.HasValue ? call.delivery_time_to.Value.ToString() : String.Empty,
                DateDelivery = call.date_delivery
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerCalls(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            DAL.Customers.CustomerCalls CustomerCallManager = new DAL.Customers.CustomerCalls();
            int userId = (int)System.Web.Security.Membership.GetUser().ProviderUserKey;
            List<calls> calls_for_curr_user = CustomerCallManager.GetCalls(userId, sidx, sord, page, rows);

            
            var CCResults = calls_for_curr_user.Select(
                a => new
                {
                    id = a.Id,
                    cell = new object[] {
                    a.Id,
                    a.date.Value.ToShortDateString(),
                    CustomerCallManager.getStatus((int)a.status),
                    CustomerCallManager.getManagerName((int)a.manager)                    
                    }
                });
            int totalRecords = CustomerCallManager.getCallsCount(userId);
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = CCResults

            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

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

        public string DeleteCall(int Id)
        {
            DAL.Customers.CustomerCalls.DeleteCall(Id);
            return "Usyo ok";
        }

        [HttpPost]
        public string SaveCall(CustomerCallModel model)
        {
            if(model.Id == 0)
            {
                //add
            }
            else
            {
                //edit
            }
            //DAL.Customers.CustomerCalls.DeleteCall(Id);
            return "Usyo ok";
        }

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