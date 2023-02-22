using Platform.Web.Models.Orders;
using Platform.Web.Services.Interfaces;

namespace Platform.Web.Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderCreatedVm> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderVm>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new NotImplementedException();
        }
    }
}
