using MangmentBS.API.Errors;
using MangmentBS.Core.Dtos.Payment;
using MangmentBS.Core.Params.Payment;
using MangmentBS.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangmentBS.API.Controllers
{
    [Authorize]
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService paymentService;
        public PaymentController(IPaymentService _paymentService)
        {
            paymentService = _paymentService;
        }
        [HttpGet("GetAllPaymentTableView")]
        public async Task<IActionResult> GetAllPaymentTableViewAsync([FromQuery] PaymentSpecificationsParams paymentSpecificationsParams)
        {
            var result = await paymentService.GetAllPaymentTableViewAsync(paymentSpecificationsParams);
            return Ok(result);
        }
        [HttpGet("GetPaymentDetails/{Id}")]
        public async Task<IActionResult> GetPaymentDetails(int Id)
        {
            var result = await paymentService.GetPaymentDetails(Id);
            if (result == null) return NotFound(new ApiError(404, $"الفتوره غير موجوده"));
            return Ok(result);
        }
        [HttpPost("SellFlatPayment")]
        public async Task<IActionResult> SellFlatPayment(SellFlatPaymentParams Params)
        {
            if (Params == null) return BadRequest(new ApiError(400, "المدخلات غير صحيحه"));
            var result = await paymentService.SellFlatPayment(Params);
            if (result == null) return NotFound(new ApiError(404, "الفتوره غير موجوده"));
            return Ok(result);
        }
    }
}
