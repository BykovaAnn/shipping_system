using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Class1
    {
        public List<calls> GetCalls()
        {
            List<calls> result = new List<calls>();
            using (DAL.shipping_systemEntities db1 = new shipping_systemEntities())
            {
                //сорт, таке, скип
                result = db1.calls.ToList();
            }
            return result;
        }

    }
}
