using MassTransit;
using Microsoft.EntityFrameworkCore;
using Service.Order.Infrastructure;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Order.Application.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly OrderDbContext _orderDb;

        public CourseNameChangedEventConsumer(OrderDbContext orderDb)
        {
            _orderDb = orderDb;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var orderitems = await _orderDb.OrderItems.Where(x=>x.ProductID==context.Message.CourseId).ToListAsync();
            orderitems.ForEach(x =>
            {
                x.UpdateOrderItem(context.Message.UpdatedName, x.PicturetUrl, x.Price);
            });
            await _orderDb.SaveChangesAsync();
        }
    }
}
