using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public bool SaveOrder(Order order)
        {
            var result = false;
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var rowEffect = 0;
                    order.OrderId = Guid.NewGuid();
                    order.OrderDate = DateTime.Now;
                    rowEffect += InsertWithTransaction(order, transaction);
                    if (rowEffect > 0 && order.OrderDetail.Count > 0)
                    {
                        foreach (OrderDetail od in order.OrderDetail)
                        {
                            od.OrderId = order.OrderId;
                            od.OrderDetailId = Guid.NewGuid();
                            rowEffect += InsertWithTransaction(od, transaction);
                        }
                    }
                    if (rowEffect < (order.OrderDetail.Count + 1))
                    {
                        transaction.Rollback();
                        result = false;
                    }
                    else
                    {
                        transaction.Commit();
                        result = true;
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return result;
        }

        public override Order GetEntityById(Guid orderId)
        {
            Order order = new Order();
            string sql = $"SELECT * FROM `order` o WHERE o.OrderId = @OrderId;" +
                        $"SELECT * FROM orderdetail od WHERE od.OrderId = @OrderId;";
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using (var multi = _dbConnection.QueryMultiple(sql, new { orderId = orderId }))
            {
                order = multi.Read<Order>().SingleOrDefault();
                if (order != null)
                {
                    order.OrderDetail = multi.Read<OrderDetail>().ToList();
                }
            }

            return order;
        }

        /// <summary>
        /// Lấy danh sách order theo số điện thoại khách hàng
        /// </summary>
        /// <param name="telephoneCustomer"></param>
        /// <returns></returns>
        public List<Order> GetOrdersByTelephoneCustomer(string telephoneCustomer)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
            var storeName = "Proc_GetOrdersByPhone";
            var parameters = new DynamicParameters();
            parameters.Add($"$TelephoneCustomer", telephoneCustomer, DbType.String);
            using (var multi = _dbConnection.QueryMultiple(storeName, parameters, commandType: CommandType.StoredProcedure))
            {

                var lstOrder = multi.Read<Order>().ToList();
                var lstOrderDetail = multi.Read<OrderDetail>().ToList();
                if (lstOrder.Count > 0)
                {
                    foreach (var order in lstOrder)
                    {
                        order.OrderDetail = lstOrderDetail.Where(x => x.OrderId == order.OrderId).ToList();
                    }
                }
                return lstOrder;
            }
        }

        public string getLast6MonthsRenvenue()
        {
            var result = _dbConnection.Query<String>($"Proc_GetLast6MonthsRenvenue", commandType: CommandType.StoredProcedure);
            return result.First();
        }

        public string getLast7DaysRenvenue()
        {
            var result = _dbConnection.Query<String>($"Proc_GetLast7DaysRenvenue", commandType: CommandType.StoredProcedure);
            return result.First();
        }

        public IEnumerable<Order> GetOrderForShipper()
        {
            var entities = _dbConnection.Query<Order>($"Proc_GetOrderForShipper", commandType: CommandType.StoredProcedure);
            // trả dữ liệu
            return entities;
        }
    }
}
