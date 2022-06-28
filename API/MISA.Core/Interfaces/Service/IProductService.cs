using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service
{
    /// <summary>
    /// interface của hàng hóa
    /// </summary>
    public interface IProductService : IBaseService<Product>
    {
        /// <summary>
        /// service lấy mã code sku mới của hàng hóa
        /// </summary>
        /// <param name="inputText">Tên hàng hóa</param>
        /// <returns>mã hàng hóa mới</returns>
        public string GetNewProductCodeSKU(string inputText);



        /// <summary>
        /// service lấy mã vạch mới
        /// </summary>
        /// <returns>mã vạch mới</returns>
        public string GetNewProductBarCode();

        /// <summary>
        /// service xóa nhiều hàng hóa
        /// </summary>
        /// <param name="listEmployeeId">danh sách id hàng hóa xóa</param>
        /// <returns></returns>
        ServiceResult DeleteMultipleProducts(string[] arrProductId);

        /// <summary>
        /// Phân trang tìm kiếm và sắp xếp
        /// </summary>
        /// <param name="pageIndex">Số trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi hiển thị trên 1 trang</param>
        /// <param name="objectFilters">Danh sách điệu kiện tìm kiếm</param>
        /// <returns>Danh sách bản ghi cần tìm kiếm</returns>
        object FilterProducts(int pageIndex, int pageSize, string objectFilters, string objectSort);

        /// <summary>
        /// lấy danh sách món an theo loại
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        List<Product> GetProcductsByCategoryID(Guid categoryId, string productName);
    }
}
