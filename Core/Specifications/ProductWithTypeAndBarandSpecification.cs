using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithTypeAndBarandSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBarandSpecification()
        {
            AddIncludes();
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
