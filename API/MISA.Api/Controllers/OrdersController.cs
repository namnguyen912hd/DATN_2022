using EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Api.Controllers
{
    [ApiController]
    public class OrdersController : BaseEntitiesController<Order>
    {
        IOrderService _baseService;
        public OrdersController(IOrderService baseService, IEmailSender emailSender) : base(baseService)
        {
            _baseService = baseService;
        }

        [HttpPost("SaveOrder")]
        public IActionResult SaveOrder([FromBody] Order order)
        {
            try
            {
                ServiceResult serviceResult = _baseService.SaveOrder(order);
                return Ok(serviceResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("sendEmail")]
        public IActionResult sendEmail(Order order)
        {
            try
            {
                _baseService.sendEmail(order);
                return Ok(Core.Enums.MISAEnum.MISACode.Success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetOrdersByTelephoneCustomer/{telephoneCustomer}")]
        public IActionResult GetOrdersByTelephoneCustomer(string telephoneCustomer)
        {
            try
            {
                List<Order> lst = _baseService.GetOrdersByTelephoneCustomer(telephoneCustomer);
                return Ok(lst);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("getLast6MonthsRenvenue")]
        public IActionResult getLast6MonthsRenvenue()
        {
            try
            {
                return Ok(_baseService.getLast6MonthsRenvenue());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getLast7DaysRenvenue")]
        public IActionResult getLast7DaysRenvenue()
        {
            try
            {
                return Ok(_baseService.getLast7DaysRenvenue());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("OrderForShipper")]
        public IActionResult GetOrderForShipper()
        {
            try
            {
                return Ok(_baseService.GetOrderForShipper());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
