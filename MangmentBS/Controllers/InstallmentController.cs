using MangmentBS.API.Errors;
using MangmentBS.Core.Dtos.Installment;
using MangmentBS.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangmentBS.API.Controllers
{
    [Authorize]
    public class InstallmentController : BaseApiController
    {
        private readonly IInstallmentService installmentService;

        public InstallmentController(IInstallmentService _installmentService)
        {
            installmentService = _installmentService;
        }
        [HttpGet("InstallmentDetails/{id}")]
        public async Task<IActionResult> GetInstallmentDetails(int id)
        {
            var result = await installmentService.GetInstallmentByIdAsync(id);
            if (result == null) return NotFound(new ApiError(404, $"رقم القصت غير صحيح"));
            return Ok(result);
        }
        [HttpPost("PayInstallment")]
        public async Task<IActionResult> PayInstallment(PayInstallmentPrams param)
        {
            var result = await installmentService.PayInstallment(param.Id);
            if (result.Status != "200")
            {
                return BadRequest(new ApiError(400, result.Message));
            }
            return Ok(result);
        }
    }
}
