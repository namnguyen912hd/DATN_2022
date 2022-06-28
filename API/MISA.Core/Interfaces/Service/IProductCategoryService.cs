using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service
{
    /// <summary>
    /// interface của nhóm hàng hóa
    /// </summary>
    public interface IProductCategoryService : IBaseService<ProductCategory>
    {
        /// <summary>
        /// Lấy danh sách loại sản phẩm và sản phâm tương ứng
        /// </summary>
        /// <returns></returns>
        List<ProductCategory> GetAllProcductByCategory();
    }
}
