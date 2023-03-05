using Service.Basket.Dtos;
using Shared.DTOs;
using System.Text.Json;

namespace Service.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userid)
        {
           var status = await _redisService.GetDb().KeyDeleteAsync(userid);
            return status ? Response<bool>.Success(200) : Response<bool>.Fail("Basket not found", 404);

        }

        public async Task<Response<BasketDto>> GetBasket(string userid)
        {
           var existbasket = await _redisService.GetDb().StringGetAsync(userid);
            if (String.IsNullOrEmpty(existbasket))
            {
                return Response<BasketDto>.Fail("Basket not found", 404);
            }
            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existbasket), 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId,JsonSerializer.Serialize(basketDto));

            return status ? Response<bool>.Success(200) : Response<bool>.Fail("Basket could not update or save", 500);
        }
    }
}
