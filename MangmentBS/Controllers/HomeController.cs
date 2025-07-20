using MangmentBS.API.Errors;
using MangmentBS.Core.Dtos.Home;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MangmentBS.API.Controllers
{
    [Authorize]
    public class HomeController : BaseApiController
    {
        private readonly IHomeService homeService;
        public HomeController(IHomeService _homeService)
        {
            homeService = _homeService;
        }
        [HttpGet("GetHomeDetails")]
        public async Task<IActionResult> GetHomeDetails()
        {
            var result = await homeService.GetHomeDetails();
            return Ok(result);
        }
        [HttpGet("GetDateTimeView")]
        public IActionResult GetDateTimeView()
        {
            return Ok(new DateTimeView());
        }
        [HttpGet("GetAgenda")]
        public async Task<IActionResult> GetAgenda([FromQuery] int? Month, [FromQuery] int? Year)
        {
            if (Month == null || Year == null)
                return BadRequest(new ApiError(400, "يرجي ادخال الشهر والسنة بشكل صحيح"));
            var agenda = await homeService.GetAgenda(Month.Value,Year.Value);
            return Ok(agenda);

        }
    }
}
