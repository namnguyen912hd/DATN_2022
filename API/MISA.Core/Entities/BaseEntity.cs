using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Core.Enums.MISAEnum;

namespace MISA.Core.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// người tạo bản ghi
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// ngày sửa mới nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// người sửa
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// chế độ của object: Add/Edit/Delete
        /// </summary>
        public EntityState EntityState { get; set; } = EntityState.AddNew;
    }
}
