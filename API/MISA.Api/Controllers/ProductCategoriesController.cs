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
    public class ProductCategoriesController : BaseEntitiesController<ProductCategory>
    {
        IProductCategoryService _baseService;
        public ProductCategoriesController(IProductCategoryService baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// lấy all product theo từng category
        /// </summary>
        /// <returns>dữ liệu hàng hóa</returns>
        [HttpGet("GetAllProductByCategory")]
        public IActionResult GetAllProcductByCategory()
        {
            var res = _baseService.GetAllProcductByCategory();
            return Ok(res);
        }
    }
}
