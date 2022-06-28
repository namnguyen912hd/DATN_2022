using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service
{
    /// <summary>
    /// interface của base service
    /// </summary>
    public interface IBaseService<TEntity>
    {
        #region method

        /// <summary>
        /// Service lấy tất cả đối tượng
        /// </summary>
        /// <returns>danh sách đối tượng</returns>
        IEnumerable<TEntity> GetEntities();

        /// <summary>
        /// Service lấy đối tượng theo ID
        /// </summary>
        /// <param name="entityId">ID đối tượng</param>
        /// <returns>thông tin đối tượng</returns>
        TEntity GetEntityById(Guid entityId);

        /// <summary>
        /// Service thêm thông tin đối tượng
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns></returns>
        ServiceResult Add(TEntity entity);

        /// <summary>
        /// Service sửa thông tin đối tượng
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns></returns>
        ServiceResult Update(TEntity entity);

        /// <summary>
        /// Service xóa đối tượng
        /// </summary>
        /// <param name="entityId">ID đối tượng</param>
        /// <returns></returns>
        ServiceResult Delete(Guid entityId);


        #endregion

    }
}
