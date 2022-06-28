using MISA.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Order : BaseEntity
    {
        public Guid OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public int ShippingCost { get; set; }
        public string ReceviedAddress { get; set; }
        public string TelephoneCustomer { get; set; }
        public string Email { get; set; }
        public int PaymentType { get; set; }
        public string Notes { get; set; }
        public string CustomerName { get; set; }
        public int TotalAmount { get; set; }
        public int? IsPrint { get; set; }
        public string ShipperName { get; set; }
        public string UserComfirmName { get; set; }
        public DateTime? ConfirmationTime { get; set; }
        public DateTime? DeliveryTime { get; set; }

        [DBNotMap]
        public List<OrderDetail>? OrderDetail { get; set; }


    }
}
