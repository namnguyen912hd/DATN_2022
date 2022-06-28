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
    [ApiController]
    public class PermissionsController : BaseEntitiesController<Permission>
    {
        IPermissionService _baseService;
        public PermissionsController(IPermissionService baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        [HttpPost("{roleId}")]
        public IActionResult AddPermissions([FromRoute] Guid roleId, [FromBody] string[] lstPermission)
        {
            try
            {
                var serviceResult = _baseService.insertPermission(roleId, lstPermission);
                return Ok(serviceResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
