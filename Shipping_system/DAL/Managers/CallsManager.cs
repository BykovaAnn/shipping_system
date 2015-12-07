using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Managers
{
    public class CallsManager
    {

        public static List<calls> GetCalls(Int32 page, Int32 perPage, String sort, String napravlenirSortirovki)
        {
            using (shipping_systemEntities dc = new shipping_systemEntities())
            {
                return dc.calls.Skip((page - 1) * perPage).Take(perPage).ToList();

            }


        }
    }
}
