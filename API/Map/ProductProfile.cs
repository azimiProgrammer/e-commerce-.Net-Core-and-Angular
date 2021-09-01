using API.Dtos.ProductDtos;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Map
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.ProductTypeName, opt => opt.MapFrom(o => o.ProductType.Name))
                .ForMember(x => x.ProductBrandName, opt => opt.MapFrom(o => o.ProductBrand.Name));
        }
    }
}
