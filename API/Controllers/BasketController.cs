using System;
using System.Threading.Tasks;
using Core.Contracts.Repository;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketConroller : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketConroller(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetAsync(string id){
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> PostAsync([FromBody]CustomerBasket basket){
            var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);

            return Ok();
        }

    }
    
}