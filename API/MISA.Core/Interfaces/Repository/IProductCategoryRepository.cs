using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    /// <summary>
    /// interface của nhóm hàng hóa
    /// </summary>
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory>
    {
        /// <summary>
        /// Lấy danh sách loại sản phẩm và sản phâm tương ứng
        /// </summary>
        /// <returns></returns>
        List<ProductCategory> GetAllProcductByCategory();
    }
}
