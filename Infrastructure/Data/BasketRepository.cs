using System;
using System.Threading.Tasks;
using Core.Contracts.Repository;
using Core.Entities;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.Data{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _dataBase;
        
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _dataBase = redis.GetDatabase();
        }
        
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _dataBase.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _dataBase.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var created = await _dataBase.StringSetAsync(customerBasket.Id, 
                    JsonConvert.SerializeObject(customerBasket), 
                    TimeSpan.FromDays(30));
                 
            if(!created) return null;

            return await GetBasketAsync(basketId: customerBasket.Id);
        }
    }
}