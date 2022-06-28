using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MISA.Core.Enums.MISAEnum;

namespace MISA.Core.Entities
{
    /// <summary>
    /// class kết quả trả về của service
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// dữ liệu trả về của service
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Thông báo kết quả trả về của service
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Mã Code của service
        /// </summary>
        public MISACode MISACode { get; set; }
    }
}
