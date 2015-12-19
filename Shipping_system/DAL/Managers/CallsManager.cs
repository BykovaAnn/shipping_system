using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Managers
{
    public class CallsManager
    {

        public static List<calls> GetCalls(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            using (shipping_systemEntities dc = new shipping_systemEntities())
            {

                return dc.calls.Skip((page - 1) * rows).Take(rows).ToList();

            }


        }
    }
}
