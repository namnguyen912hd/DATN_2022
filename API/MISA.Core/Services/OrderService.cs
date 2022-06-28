using EmailService;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        #region declare

        IOrderRepository _orderRepository;

        private readonly IEmailSender _emailSender;
        #endregion

        #region contructor
        public OrderService(IOrderRepository orderRepository,  IEmailSender emailSender) : base(orderRepository)
        {
            _orderRepository = orderRepository;

            _emailSender = emailSender;
        }

        #endregion

        #region method
        public ServiceResult SaveOrder(Order order)
        {
            // validate dữ liệu
            var isValidate = Validate(order);
            if (isValidate == true)
            {
                _serviceResult.Data = _orderRepository.SaveOrder(order);
                _serviceResult.MISACode = Enums.MISAEnum.MISACode.IsValid;
            }
            return _serviceResult;
        }

        public List<Order> GetOrdersByTelephoneCustomer(string telephoneCustomer)
        {
            return _orderRepository.GetOrdersByTelephoneCustomer(telephoneCustomer);
        }

        public void sendEmail(Order order)
        {
            try
            {
                string content = System.IO.File.ReadAllText(@"D:\ĐATN\program\API\MISA.Core\template\order.html");
                content = content.Replace("{{CustomerName}}", order.CustomerName);
                content = content.Replace("{{Phone}}", order.TelephoneCustomer);
                content = content.Replace("{{Address}}", order.ReceviedAddress);
                content = content.Replace("{{Total}}", order.TotalAmount.ToString());
                var message = new Message(new string[] { order.Email }, "Confirmation Order", content);
                _emailSender.SendEmail(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string getLast6MonthsRenvenue()
        {
            return _orderRepository.getLast6MonthsRenvenue();
        }

        public string getLast7DaysRenvenue()
        {
            return _orderRepository.getLast7DaysRenvenue();
        }

        public IEnumerable<Order> GetOrderForShipper()
        {
            return _orderRepository.GetOrderForShipper();
        }
        #endregion
    }
}
