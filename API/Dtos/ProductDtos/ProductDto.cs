using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.ProductDtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string ProductTypeName { get; set; }
        public long ProductTypeId { get; set; }
        public string ProductBrandName { get; set; }
        public long ProductBrandId { get; set; }
    }
}
