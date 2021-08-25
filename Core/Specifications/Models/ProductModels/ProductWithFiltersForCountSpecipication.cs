using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.Models.ProductModels
{
    public class ProductWithFiltersForCountSpecipication : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecipication(ProductSpecParams productParams)
            : base(x =>
                 (string.IsNullOrEmpty(productParams.Search) || x.Name.Trim().ToLower().Contains(productParams.Search)) &&
                 (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                 (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            
        }
    }
}
