using API.Dtos.ProductBrandDtos;
using AutoMapper;
using Core.Entities;

namespace API.Map
{
    public class ProductBrandProfile : Profile
    {
        public ProductBrandProfile()
        {
            CreateMap<ProductBrand, ProductBrandDto>();
        }
    }
}
