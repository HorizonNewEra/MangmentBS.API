using MangmentBS.API.Errors;
using MangmentBS.Core.Dtos.Building;
using MangmentBS.Core.Params.Building;
using MangmentBS.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangmentBS.API.Controllers
{
    [Authorize]
    public class BuildingController : BaseApiController
    {
        private readonly IBuildingService buildingService;

        public BuildingController(IBuildingService _buildingService)
        {
            buildingService = _buildingService;
        }
        [HttpGet("BuildingTableView")]
        public async Task<IActionResult> GetAllBuildingTableView([FromQuery] BuildingSpecificationsParams Params)
        {
            var result = await buildingService.GetAllBuildingTableViewAsync(Params);
            return Ok(result);
        }
        [HttpGet("BuildingTableView/{Id}")]
        public async Task<IActionResult> GetAllBuildingsAsync(int? Id)
        {
            if(Id == null) return BadRequest(new ApiError(400, "رقم العقار مطلوب"));
            var result= await buildingService.GetBuildingTableViewByIdAsync(Id.Value);
            if (result == null) return NotFound(new ApiError(404, $"هذا العقار غير موجود"));
            return Ok(result);
        }
        [HttpGet("BuildingDetailsView/{Id}")]
        public async Task<IActionResult> GetBuildingDetails(int Id)
        {
            var result = await buildingService.GetBuildingDetails(Id);
            if (result == null) return NotFound(new ApiError(404, $"هذا العقار غير موجود"));
            return Ok(result);
        }
        [HttpPost("AddBuilding")]
        public async Task<IActionResult> AddBuilding(AddBuildingView view)
        {
            var result = await buildingService.AddBuilding(view);
            if (result.Status != "200")
            {
                return BadRequest(new ApiError(400, result.Message));
            }
            return Ok(result);
        }
        [HttpPost("EditBuilding")]
        public async Task<IActionResult> EditBuilding(EditBuildingView view)
        {
            var result = await buildingService.EditBuilding(view);
            if (result.Status != "200")
            {
                return BadRequest(new ApiError(400, result.Message));
            }
            return Ok(result);
        }
    }
}
