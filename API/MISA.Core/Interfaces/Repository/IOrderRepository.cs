using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        /// <summary>
        /// Thêm order và list danh sách order Detail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// ttkien
        bool SaveOrder(Order order);

        /// <summary>
        /// Lấy danh sách order theo số điện thoại khách hàng
        /// </summary>
        /// <param name="telephoneCustomer"></param>
        /// <returns></returns>
        List<Order> GetOrdersByTelephoneCustomer(string telephoneCustomer);

        string getLast6MonthsRenvenue();
        string getLast7DaysRenvenue();

        IEnumerable<Order> GetOrderForShipper();
    }
}
