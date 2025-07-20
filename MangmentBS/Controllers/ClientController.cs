using MangmentBS.API.Errors;
using MangmentBS.Core.Dtos.Client;
using MangmentBS.Core.Params.Clients;
using MangmentBS.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MangmentBS.API.Controllers
{
    [Authorize]
    public class ClientController : BaseApiController
    {
        private readonly IClientServices clientService;

        public ClientController(IClientServices _clientService)
        {
            clientService = _clientService;
        }
        [HttpGet("ClientTableView")]
        public async Task<IActionResult> GetAllBuildingTableView([FromQuery] ClientSpecificationsParams Params)
        {
            var result = await clientService.GetAllClientTableViewAsync(Params);
            return Ok(result);
        }
        [HttpGet("ClientTableView/{id}")]
        public async Task<IActionResult> GetAllClientAsync(string? id)
        {
            if (id == null) return BadRequest(new ApiError(400, "الرقم القومي للعميل مطلوب"));
            var result = await clientService.GetClientTableViewByIdAsync(id);
            if (result == null) return NotFound(new ApiError(404, $"هذا العميل غير موجود"));
            return Ok(result);
        }
        [HttpGet("ClientDetailsView/{id}")]
        public async Task<IActionResult> GetClientDetails(string id)
        {
            var result = await clientService.GetClientDetails(id);
            if (result == null) return NotFound(new ApiError(404, $"هذا العميل غير موجود"));
            return Ok(result);
        }
        [HttpGet("ClientInstallmentView/{id}")]
        public async Task<IActionResult> GetClientInstallment(int id)
        {
            var result = await clientService.GetClientPaymentInstallments(id);
            if (result == null) return NotFound(new ApiError(404, $"هذة الفتوره غير موجودة"));
            return Ok(result);
        }
        [HttpPost("AddClient")]
        public async Task<IActionResult> AddBuilding(AddClientView view)
        {
            var result = await clientService.AddClient(view);
            if (result.Status != "200")
            {
                return BadRequest(new ApiError(400, result.Message));
            }
            return Ok(result);
        }
        [HttpPost("EditClient")]
        public async Task<IActionResult> EditClient(EditClientView view)
        {
            var result = await clientService.EditClient(view);
            if (result.Status != "200")
            {
                return BadRequest(new ApiError(400, result.Message));
            }
            return Ok(result);
        }
        [HttpGet("GetAllClientsData")]
        public async Task<IActionResult> GetAllClientsData()
        {
            var result = await clientService.GetAllClientsDataAsync();
            if (result == null) return NotFound(new ApiError(404, "لا يوجد عملاء مسجلين"));
            return Ok(result);
        }
    }
}
