using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public int insertPermission(Guid roleId, string[] lstPermission)
        {
            var rowAffects = 0;
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    string sqlQuery = "";
                    for (int i = 0; i < lstPermission.Length; i++)
                    {
                        sqlQuery = $"DELETE from permission WHERE permission.RoleId = '{roleId}' AND permission.PermissionCode = '{lstPermission[i]}' ; INSERT INTO permission(PermissionId, PermissionCode, RoleId) VALUES(UUID(), @permissionCode, '{roleId}') ; ";
                        // thực thi commandtext                    
                        rowAffects += _dbConnection.Execute(sqlQuery, new { @permissionCode = lstPermission[i] }, transaction, commandType: CommandType.Text);
                    }
                    if (rowAffects > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }

                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            //trả về kết quả(số bản ghi xóa được)
            return rowAffects;
        }
    }
}
