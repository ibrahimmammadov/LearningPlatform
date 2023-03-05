using MassTransit;
using Service.Basket.Dtos;
using Service.Basket.Services;
using Shared.Messages;
using Shared.Services;
using System.Text.Json;

namespace Service.Basket.Consumers
{
    public class BasketCourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly RedisService _redisService;
        private readonly ISharedIdentityService _sharedIdentityService;


        public BasketCourseNameChangedEventConsumer(RedisService redisService, ISharedIdentityService sharedIdentityService)
        {
             _redisService = redisService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
           var getbasket =  await _redisService.GetDb().StringGetAsync(context.Message.UserId);
           var desrializebasket =  JsonSerializer.Deserialize<BasketDto>(getbasket);
            desrializebasket.BasketItems.ForEach(x =>
            {
                x.CourseName = context.Message.UpdatedName;
            });
            await _redisService.GetDb().StringSetAsync(context.Message.UserId, JsonSerializer.Serialize(desrializebasket));
        }
    }
}
