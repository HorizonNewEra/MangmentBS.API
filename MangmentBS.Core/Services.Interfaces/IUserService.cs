using MangmentBS.Core.Dtos.Errors;
using MangmentBS.Core.Dtos.Users;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Params.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<PaginationResponse<UserTableView>> GetAllUserTableViewAsync(UserSpecificationsParams Params);
        Task<UserData> LoginAsync(LogInView logInView);
        Task<UserData> RegisterAsync(RegisterView registerView);
        Task<bool> EmailIsExist(string Email);
        Task<bool> UserNameIsExist(string username);
        Task<bool> DeActiveUser(string Id);
        Task<bool> ActiveUser(string Id);
        Task<bool> DeleteUser(string Id);
        Task<bool> EditUserAdmin(EditUserAdmin editUser);
        Task<UserData> EditUserProfile(EditUserProfile editUser);
        Task<ErrorResponce> AddUser(AddUserView addUser);
    }
}
