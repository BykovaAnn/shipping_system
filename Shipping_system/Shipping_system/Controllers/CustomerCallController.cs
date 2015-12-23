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
        //Метод используется для отображения страницы
        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            return View();
        }

        //Метод используется для отображения данных о заказе
        //Получает id заказа
        //Возращает сериализованные данные о заказе
        public JsonResult GetCall(Int32 callID)
        {
            DAL.Customers.CustomerCalls CustomerCallManager = new DAL.Customers.CustomerCalls();
            calls call = CustomerCallManager.GetCall(callID);
            CustomerCallModel model = new CustomerCallModel()
            {
                Id = call.Id,
                DeliveryFrom = call.delivery_from,
                DeliveryTo = call.delivery_to,
                DeliveryTimeFrom =  call.delivery_time_from.ToString(),
                DeliveryTimeTo = call.delivery_time_to.ToString(),
                DateDelivery_s = call.date_delivery.ToShortDateString()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //Метод используется для редактирования данных о заказе или добавления нового
        //Получает id заказа и данные из формы
        //Возращает сообщения пользователю
        public JsonResult SaveCall(String callID,
            String DeliveryFrom,
            String DeliveryTo,
            String DateDelivery,
            String DeliveryTimeFrom,
            String DeliveryTimeTo)
        {
            DAL.Customers.CustomerCalls CustomerCallManager = new DAL.Customers.CustomerCalls();
            if (String.IsNullOrEmpty(callID) || Int32.Parse(callID) == 0)
            {
                CustomerCallManager.AddCall((int)System.Web.Security.Membership.GetUser().ProviderUserKey,
                    DeliveryFrom.TrimEnd(' '),
                    DeliveryTo.TrimEnd(' '),
                    DateTime.Parse(DateDelivery),
                    DeliveryTimeFrom.TrimEnd(' '),
                    DeliveryTimeTo.TrimEnd(' '));
                var jsonData = new
                {
                    result = "Call is added!"
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (CustomerCallManager.GetCall(Int32.Parse(callID)).status == 1)
                {
                    CustomerCallManager.EditCall(Int32.Parse(callID),
                        DeliveryFrom.TrimEnd(' '),
                        DeliveryTo.TrimEnd(' '),
                        DateTime.Parse(DateDelivery),
                        DeliveryTimeFrom.TrimEnd(' '),
                        DeliveryTimeTo.TrimEnd(' '));
                    var jsonData = new
                    {
                        result = "Call is updated!"
                    };
                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var jsonData = new
                    {
                        result = "Call can not be updated, it's not open status!"
                    };
                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                }
            }
        }

        //Метод используется для таблицы
        //Получает данные для сортировки и количества отображаемых строк
        //Возращает сериализованные данные о заказах
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
                    a.date.ToShortDateString(),
                    CustomerCallManager.getStatus((int)a.status),
                    CustomerCallManager.getManagerName(a.manager)                    
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

        //Метод используется для изменения статуса заказа
        //Получает id заказа
        //Возращает сообщение пользователю
        public JsonResult DeleteCall(int Id)
        {
            DAL.Customers.CustomerCalls CustomerCallManager = new DAL.Customers.CustomerCalls();
            calls call = CustomerCallManager.GetCall(Id);
            if (call.status == 1)
            {
                CustomerCallManager.DeleteCall(Id);
                var jsonData = new
                {
                    result = "Call is cancaled!"
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var jsonData = new
                {
                    result = "Call can not be canceled, it's not open status!"
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }
    }   
}