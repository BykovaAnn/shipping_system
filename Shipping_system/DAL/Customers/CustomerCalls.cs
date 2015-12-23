using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Customers
{
    //Класс используется работы с базой данных
    public class CustomerCalls
    {
        //Метод используется для таблицы
        //Получает id менеджера
        //Возращает имя и фамилию менеджера
        public string getManagerName(int? id)
        {
            using (shipping_systemEntities db = new DAL.shipping_systemEntities())
            {
                if (id.HasValue)
                {
                    return db.UserProfile.Where(x => x.UserId == id).ToList()[0].LastName + " " + db.UserProfile.Where(x => x.UserId == id).ToList()[0].FirstName;
                }
                else
                {
                    return "No manager yet";
                }
            }
        }

        //Метод используется для таблицы
        //Получает id статуса
        //Возращает строковое название статуса
        public string getStatus(int id)
        {
            using (shipping_systemEntities db = new DAL.shipping_systemEntities())
            {
                return db.status.Where(x => x.Id == id).ToList()[0].title;
            }
        }

        //Метод используется для редактирования
        //Получает id заказа
        //Возращает полную информацию о заказе
        public calls GetCall(int idCall)
        {
            using (shipping_systemEntities db = new DAL.shipping_systemEntities())
            {                
                calls result = db.calls.FirstOrDefault(x => x.Id == idCall);
                result.delivery_from.TrimEnd(' ');
                result.delivery_to.TrimEnd(' ');
                return result;                 
            }
        }

        //Метод используется для отображения строк таблицы
        //Получает id текущего пользователя
        //Возращает общее количество всех его заказов
        public Int32 getCallsCount(int id)
        {
            Int32 result = 0;
            using (shipping_systemEntities db = new DAL.shipping_systemEntities())
            {
                result = db.calls.Where(c => c.cutomer == id).Count();
            }
            return result;
        }

        //Метод используется для таблицы
        //Получает id текущего пользователя, данные для сортировки, страницу и количество строк на страницу в таблице
        //Возращает информацию о заказах пользователя для страницы
        public List<calls> GetCalls(int id, string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            List<calls> result = new List<calls>();

            using (DAL.shipping_systemEntities db1 = new shipping_systemEntities())
            {
                result = db1.calls.Where(c => c.cutomer == id).OrderBy(x=>x.Id).Skip(rows * (page - 1)).Take(rows).ToList();
            }
            return result;
        }

        //Метод используется для добавления записи в базу
        //Получает id текущего пользователя и информацию о заказе
        public void AddCall(int id_user, string d_from, string d_to, DateTime d_date, String d_t_from, String d_t_to)
        {
            using (DAL.shipping_systemEntities db1 = new shipping_systemEntities())
            {
                db1.calls.Add(new calls
                {
                    status = 1,
                    manager = null,
                    cutomer = id_user,
                    date = DateTime.Now,
                    date_delivery = d_date,
                    delivery_from = d_from.TrimEnd(' '),
                    delivery_to = d_to.TrimEnd(' '),
                    delivery_time_from = TimeSpan.Parse(d_t_from),
                    delivery_time_to =TimeSpan.Parse(d_t_to)
                });
                db1.SaveChanges();
            }
        }

        //Метод используется для редактирования данных в базе
        //Получает id заказа и обновленную информацию о нем
        public void EditCall(int id_call, string d_from, string d_to, DateTime d_date, String d_t_from, String d_t_to)
        {
            using (DAL.shipping_systemEntities db1 = new shipping_systemEntities())
            {
                
                calls obj = db1.calls.Find(id_call);
                if (obj.status == 1)
                {
                    obj.date = DateTime.Now;
                    obj.date_delivery = d_date;
                    obj.delivery_from = d_from.TrimEnd(' ');
                    obj.delivery_to = d_to.TrimEnd(' '); 
                    obj.delivery_time_from = TimeSpan.Parse(d_t_from);
                    obj.delivery_time_to = TimeSpan.Parse(d_t_to);
                    db1.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db1.SaveChanges();
                }
                else
                {
                    
                }
            }
        }

        //Метод используется для обозначения заказа как отмененный
        //Получает id заказа
        public void DeleteCall(int id_call)
        {
            using (DAL.shipping_systemEntities db1 = new shipping_systemEntities())
            {
                calls obj = db1.calls.Find(id_call);
                obj.status = 3;
                db1.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db1.SaveChanges();
            }
        }
    }
}
