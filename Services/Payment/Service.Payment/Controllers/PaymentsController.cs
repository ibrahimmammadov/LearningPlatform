using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Payment.Models;
using Shared.ControllerBases;
using Shared.DTOs;

namespace Service.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            return CreateActionResult(Response<NoContent>.Success(200));
        }
        
    }
}
