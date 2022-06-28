using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class PermissionService : BaseService<Permission>, IPermissionService
    {
        #region declare

        IPermissionRepository _permissionRepository;

        #endregion

        #region contructor
        public PermissionService(IPermissionRepository permissionRepository) : base(permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public ServiceResult insertPermission(Guid roleId, string[] lstPermission)
        {

            _serviceResult.Data = _permissionRepository.insertPermission(roleId,lstPermission);
            _serviceResult.MISACode = Enums.MISAEnum.MISACode.IsValid;
            return _serviceResult;
        }

        #endregion
    }
}
