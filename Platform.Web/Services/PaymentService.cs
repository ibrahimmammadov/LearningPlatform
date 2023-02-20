using Platform.Web.Models.Payment;
using Platform.Web.Services.Interfaces;

namespace Platform.Web.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            throw new NotImplementedException();
        }
    }
}
