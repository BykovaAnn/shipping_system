using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping_system.Models
{
    public class CustomerCallModel
    {
        //Id заказа
        public int Id { get; set; }
        //Id статуса заказа
        public int Status { get; set; }
        //Id менеджера, обрабатывающего заказ
        public int Manager { get; set; }
        //дата заказа
        public DateTime Date { get; set; }
        //дата выполнения заказа
        public DateTime DateDelivery { get; set; }
        //строковое представление даты
        public string DateDelivery_s { get; set; }
        //адрес отправки
        public string DeliveryFrom { get; set; }
        //адрес доставки
        public string DeliveryTo { get; set; }
        //информация об удобном времени доставки
        public String DeliveryTimeFrom { get; set; }
        public String DeliveryTimeTo { get; set; }
    }
}
