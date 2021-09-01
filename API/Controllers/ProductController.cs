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
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithTypeAndBarandSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecipication(productParams);

            var totalItems = await _productRepository.CountAsync(countSpec);
            var products = await _productRepository.LsitAsync(spec);
            var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return Ok(new Pagination<ProductDto>(
                productParams.PageNumber,
                productParams.PageSize,
                totalItems,
                data
           ));
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> Get(long id)
        {
            var spec = new ProductWithTypeAndBarandSpecification(id);
            var product = await _productRepository.GetEntityWithSpec(spec);
            return Ok(_mapper.Map<ProductDto>(product));
        }
    }
}
