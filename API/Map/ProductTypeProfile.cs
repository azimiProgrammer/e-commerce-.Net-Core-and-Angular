using API.Dtos.ProductTypeDtos;
using AutoMapper;
using Core.Entities;

namespace API.Map
{
    public class ProductTypeProfile : Profile
    {
        public ProductTypeProfile()
        {
            CreateMap<ProductType, ProductTypeDto>();
        }
    }
}
