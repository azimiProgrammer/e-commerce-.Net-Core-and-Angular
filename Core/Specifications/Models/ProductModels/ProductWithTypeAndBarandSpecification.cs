using Core.Entities;
using Core.Specifications.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.Models.ProductModels
{
    public class ProductWithTypeAndBarandSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBarandSpecification(ProductSpecParams productParams)
            :base(x => 
                (string.IsNullOrEmpty(productParams.Search) || x.Name.Trim().ToLower().Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            AddIncludes();
            AddDynamicOrderBy(productParams.Sort);
            ApplyPaging(productParams.PageSize * (productParams.PageNumber - 1),
                productParams.PageSize);
        }

        public ProductWithTypeAndBarandSpecification(long id)
            :base(x => x.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(a => a.ProductBrand);
            AddInclude(a => a.ProductType);
        }
    }
}
