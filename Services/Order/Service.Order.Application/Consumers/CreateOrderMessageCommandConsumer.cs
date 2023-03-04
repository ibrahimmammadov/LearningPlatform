using MassTransit;
using Service.Order.Domain.OrderAggregate;
using Service.Order.Infrastructure;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Order.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        private readonly OrderDbContext _context;

        public CreateOrderMessageCommandConsumer(OrderDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var address = new Domain.OrderAggregate.Address(context.Message.Address.Province,context.Message.Address.District,context.Message.Address.Street,
                context.Message.Address.ZipCode,context.Message.Address.Line);
            Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(context.Message.BuyerId, address);
            context.Message.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
