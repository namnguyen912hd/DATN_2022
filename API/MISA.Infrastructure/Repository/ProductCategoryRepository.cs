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
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<ProductCategory> GetAllProcductByCategory()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
            var storeName = "Proc_GetAllProcductByCategory";
            using (var multi = _dbConnection.QueryMultiple(storeName, commandType: CommandType.StoredProcedure))
            {
                var lstCate = multi.Read<ProductCategory>().ToList();
                var lstProduct = multi.Read<Product>().ToList();
                if (lstCate.Count > 0)
                {
                    foreach (var cate in lstCate)
                    {
                        cate.lstProduct = lstProduct.Where(x => x.ProductCategoryId == cate.ProductCategoryId).ToList();
                    }
                }
                return lstCate;
            }
        }
    }
}
