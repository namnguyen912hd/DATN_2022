using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface IPermissionRepository : IBaseRepository<Permission>
    {
        int insertPermission(Guid roleId, string[] lstPermission);
    }
}
