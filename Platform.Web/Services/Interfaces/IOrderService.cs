using Platform.Web.Models.Orders;

namespace Platform.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedVm> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        Task<OrderSuspendVm> SuspendOrder (CheckoutInfoInput checkoutInfoInput);
        Task<List<OrderVm>> GetOrders ();
    }
}
