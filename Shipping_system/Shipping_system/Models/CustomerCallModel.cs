using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping_system.Models
{
    public class CustomerCallModel//:IDisposable
    {

        public int Id { get; set; }
        public int Status { get; set; }
        public int Manager { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateDelivery { get; set; }
        public string DeliveryFrom { get; set; }
        public string DeliveryTo { get; set; }
        public String DeliveryTimeFrom { get; set; }
        public String DeliveryTimeTo { get; set; }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
