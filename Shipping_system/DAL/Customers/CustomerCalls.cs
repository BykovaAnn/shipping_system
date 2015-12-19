using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Customers
{
    public class CustomerCalls//:IDisposable
    {
        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public string getManagerName(int id)
        {
            using (shipping_systemEntities db = new DAL.shipping_systemEntities())
            {
                return db.UserProfile.Where(x => x.UserId == id).ToList()[0].LastName + " " + db.UserProfile.Where(x => x.UserId == id).ToList()[0].FirstName;
            }
        }

        public string getStatus(int id)
        {
            using (shipping_systemEntities db = new DAL.shipping_systemEntities())
            {
                return db.status.Where(x => x.Id == id).ToList()[0].title;
            }
        }

        public calls GetCall(int idCall)
        {
            using (shipping_systemEntities db = new DAL.shipping_systemEntities())
            {
                calls result = db.calls.FirstOrDefault(x => x.Id == idCall);
                return result;                 
            }
        }


        public  Int32 getCallsCount(int id)
        {
            Int32 result = 0;
            using (shipping_systemEntities db = new DAL.shipping_systemEntities())
            {
                result = db.calls.Where(c => c.cutomer.HasValue && c.cutomer.Value == id).Count();
            }
            return result;
        }

        public List<calls> GetCalls(int id, string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            List<calls> result = new List<calls>();

            using (DAL.shipping_systemEntities db1 = new shipping_systemEntities())
            {
                //.OrderBy(sidx + " " + sord).
                result = db1.calls.Where(c => c.cutomer.HasValue && c.cutomer.Value == id).OrderBy(x=>x.Id).Skip(rows * (page - 1)).Take(rows).ToList();
            }
            return result;
        }

        public static void AddCall(int id_user, string d_from, string d_to, DateTime? d_date, String d_t_from, String d_t_to)
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
                    delivery_from = d_from,
                    delivery_to = d_to,
                    delivery_time_from = TimeSpan.Parse(d_t_from),
                    delivery_time_to =TimeSpan.Parse(d_t_to)
                });
                db1.SaveChanges();
            }
        }

        public static void DeleteCall(int id_call)
        {
            using (DAL.shipping_systemEntities db1 = new shipping_systemEntities())
            {
                calls obj = db1.calls.Find(id_call);
                db1.calls.Remove(obj);
                db1.SaveChanges();
            }
        }
    }
}
