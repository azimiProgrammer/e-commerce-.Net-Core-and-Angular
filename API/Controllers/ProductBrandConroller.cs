using API.Dtos.ProductBrandDtos;
using API.Dtos.ProductDtos;
using API.Helper;
using AutoMapper;
using Core.Contracts.Repository;
using Core.Entities;
using Core.Specifications.Models.ProductModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductBrandController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IMapper _mapper;

        public ProductBrandController(IGenericRepository<ProductBrand> productBrandRepository, IMapper mapper)
        {
            _productBrandRepository = productBrandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductBrandDto>>> GetProducts()
        {
            var brands = await _productBrandRepository.ListAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<ProductBrandDto>>(brands));
        }
    }
}
