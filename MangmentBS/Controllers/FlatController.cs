using MangmentBS.API.Errors;
using MangmentBS.Core.Dtos.Flat;
using MangmentBS.Core.Params.Flat;
using MangmentBS.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MangmentBS.API.Controllers
{
    [Authorize]
    public class FlatController : BaseApiController
    {
        private readonly IFlatService flatService;
        public FlatController(IFlatService _flatService)
        {
            flatService = _flatService;
        }
        [HttpGet("FlatTableView")]
        public async Task<IActionResult> GetAllFlatTableView([FromQuery] FlatSpecificationsParams Params)
        {
            var result = await flatService.GetAllFlatTableViewAsync(Params);
            return Ok(result);
        }
        [HttpGet("FlatTableView/{id}")]
        public async Task<IActionResult> GetAllFlatAsync(int? id)
        {
            if (id == null) return BadRequest(new ApiError(400, "رقم تعريف الشقة مطلوب"));
            var result = await flatService.GetFlatTableViewByIdAsync(id.Value);
            if (result == null) return NotFound(new ApiError(404, $"هذة الشقة غير متوفرة"));
            return Ok(result);
        }
        [HttpGet("FlatDetailsView/{id}")]
        public async Task<IActionResult> GetFlatDetails(int id)
        {
            var result = await flatService.GetFlatDetails(id);
            if (result == null) return NotFound(new ApiError(404, $"هذة الشقة غير متوفرة"));
            return Ok(result);
        }
        [HttpPost("AddFlat")]
        public async Task<IActionResult> AddFlat(AddFlatView view)
        {
            var result = await flatService.AddFlat(view);
            if (result.Status != "200")
            {
                return BadRequest(new ApiError(400, result.Message));
            }
            return Ok(result);
        }
        [HttpPost("EditFlat")]
        public async Task<IActionResult> EditFlat(EditFlatView view)
        {
            var result = await flatService.EditFlat(view);
            if (result.Status != "200")
            {
                return BadRequest(new ApiError(400, result.Message));
            }
            return Ok(result);
        }
    }
}
