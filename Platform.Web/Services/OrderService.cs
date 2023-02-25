using Platform.Web.Models.Orders;
using Platform.Web.Models.Payment;
using Platform.Web.Services.Interfaces;
using Shared.DTOs;
using Shared.Services;

namespace Platform.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly ISharedIdentityService _sharedIdentityService;
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;

        public OrderService(ISharedIdentityService sharedIdentityService, IPaymentService paymentService, HttpClient httpClient, IBasketService basketService)
        {
            _sharedIdentityService = sharedIdentityService;
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
        }

        public async Task<OrderCreatedVm> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice
            };
            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderCreatedVm() { Error = "Payment not received", IsSuccessful = false };
            }

            var orderCreateInput = new CreateOrderInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new CreateAddressInput { Province = checkoutInfoInput.Province, District = checkoutInfoInput.District, Street = checkoutInfoInput.Street, Line = checkoutInfoInput.Line, ZipCode = checkoutInfoInput.ZipCode },
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new CreateOrdemItemInput { ProductId = x.CourseId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.CourseName };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var response = await _httpClient.PostAsJsonAsync<CreateOrderInput>("orders", orderCreateInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedVm() { Error = "Order could not be created", IsSuccessful = false };
            }

            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedVm>>();

            orderCreatedViewModel.Data.IsSuccessful = true;
            await _basketService.Delete();
            return orderCreatedViewModel.Data;
        }

        public async Task<List<OrderVm>> GetOrders()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderVm>>>("orders");

            return response.Data;
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new NotImplementedException();
        }
    }
}
