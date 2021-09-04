using API.Dtos.ProductTypeDtos;
using AutoMapper;
using Core.Contracts.Repository;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductTypeController : BaseApiController
    {
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductTypeController(IGenericRepository<ProductType> productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductTypeDto>>> GetProducts()
        {
            var types = await _productTypeRepository.ListAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<ProductTypeDto>>(types));
        }
    }
}
