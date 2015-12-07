using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping_system.Models;

namespace Shipping_system.DBContext
{
    public class CCContext: DbContext
    {
        public DbSet<CustomerCallModel> CCs { get; set; }
    }
}
