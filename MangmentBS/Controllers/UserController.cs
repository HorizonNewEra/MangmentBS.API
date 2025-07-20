using MangmentBS.API.Errors;
using MangmentBS.Core;
using MangmentBS.Core.Dtos.Users;
using MangmentBS.Core.Entities;
using MangmentBS.Core.Params.Users;
using MangmentBS.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;

namespace MangmentBS.API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService userService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;

        public UserController(IUserService _userService, IUnitOfWork _unitOfWork, ITokenService _tokenService)

        {
            userService = _userService;
            unitOfWork = _unitOfWork;
            tokenService = _tokenService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LogInView log)
        {
            var user = await userService.LoginAsync(log);
            if (user == null)
            {
                return Unauthorized(new ApiError(StatusCodes.Status401Unauthorized, "تأكد من اسم المستخدم او كلمة السر"));
            }
            return Ok(user);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterView register)
        {
            var user = await userService.RegisterAsync(register);
            if (user == null)
            {
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest, "هذا المستخدم موجود"));
            }
            return Ok(user);
        }
        [Authorize]
        [HttpPost("DeActive")]
        public async Task<IActionResult> DeActive(string id)
        {
            if (!await userService.DeActiveUser(id))
            {
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest, "يرجي اعاده العملية"));
            }
            return Ok(new { Status = StatusCodes.Status200OK, message = "تم الغاء التفعيل" });
        }
        [Authorize]
        [HttpPost("Active")]
        public async Task<IActionResult> Active(string id)
        {
            if (!await userService.DeActiveUser(id))
            {
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest, "يرجي اعاده العملية"));
            }
            return Ok(new { Status = StatusCodes.Status200OK, message = "تم التفعيل" });
        }
        [Authorize]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!await userService.DeleteUser(id))
            {
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest, "يرجي اعاده العملية"));
            }
            return Ok(new { Status = StatusCodes.Status200OK, message = "تم الحذف" });
        }
        [Authorize]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserView view)
        {
            var result = await userService.AddUser(view);
            if (result.Status != "200")
            {
                return BadRequest(new ApiError(400, result.Message));
            }
            return Ok(result);
        }
        [Authorize]
        [HttpPost("EditUserAdmin")]
        public async Task<IActionResult> EditUserAdmin(EditUserAdmin editUser)
        {
            if (!await userService.EditUserAdmin(editUser))
            {
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest, "يرجي اعاده العملية"));
            }
            return Ok(new { Status = StatusCodes.Status200OK, message = "تم التعديل" });
        }
        [Authorize]
        [HttpPost("EditUserProfile")]
        public async Task<IActionResult> EditUserProfile(EditUserProfile editUser)
        {
            var user = await userService.EditUserProfile(editUser);
            if (user == null)
            {
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest, "يرجي التحقق من البيانات"));
            }
            return Ok(user);
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var id = User.FindFirstValue(ClaimTypes.Email);
            if (id == null) return BadRequest(new ApiError(StatusCodes.Status400BadRequest));
            var users = await unitOfWork.Repository<User, string>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user is null) return BadRequest(new ApiError(StatusCodes.Status400BadRequest));
            return Ok(new UserData
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role,
                IsActive = user.IsActive,
                Token = await tokenService.CreateTokenAsync(user)
            });
        }
        [Authorize]
        [HttpGet("GetAllUserTableView")]
        public async Task<IActionResult> GetAllUserTableView([FromQuery] UserSpecificationsParams Params)
        {
            var users = await userService.GetAllUserTableViewAsync(Params);
            if (users == null)
            {
                return BadRequest(new ApiError(StatusCodes.Status400BadRequest, "لا يوجد مستخدمين"));
            }
            return Ok(users);
        }
    }
}
