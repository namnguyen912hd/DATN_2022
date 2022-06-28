using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string  Tel { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Guid RoleId { get; set; }

        public string lstPermission { get; set; }
    }
}
