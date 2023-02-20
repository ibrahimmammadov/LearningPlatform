using MediatR;
using Service.Order.Application.Commands;
using Service.Order.Application.Dtos;
using Service.Order.Domain.OrderAggregate;
using Service.Order.Infrastructure;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newaddress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode, request.Address.Line);
            Domain.OrderAggregate.Order neworder = new Domain.OrderAggregate.Order(request.BuyerId,newaddress);
            request.OrderItems.ForEach(x =>
           {
               neworder.AddOrderItem(x.ProductID, x.ProductName, x.Price, x.PicturetUrl);
           });
            await _context.Orders.AddAsync(neworder);
            await _context.SaveChangesAsync();
            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = neworder.Id }, 200);
        }
    }
}
