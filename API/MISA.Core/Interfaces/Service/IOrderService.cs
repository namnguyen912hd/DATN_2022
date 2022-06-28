using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service
{
    public interface IOrderService : IBaseService<Order>
    {
        /// <summary>
        /// Save order và OrderDetail
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// ttkien
        ServiceResult SaveOrder(Order order);

        /// <summary>
        /// Lấy danh sách order theo số điện thoại khách hàng
        /// </summary>
        /// <param name="telephoneCustomer"></param>
        /// <returns></returns>
        List<Order> GetOrdersByTelephoneCustomer(string telephoneCustomer);

        void sendEmail(Order order);

        string getLast6MonthsRenvenue();
        string getLast7DaysRenvenue();

        IEnumerable<Order> GetOrderForShipper();
    }

}
