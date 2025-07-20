using AutoMapper;
using MangmentBS.Core;
using MangmentBS.Core.Dtos.Building;
using MangmentBS.Core.Dtos.Errors;
using MangmentBS.Core.Dtos.Users;
using MangmentBS.Core.Entities;
using MangmentBS.Core.Helper;
using MangmentBS.Core.Params.Users;
using MangmentBS.Core.Services.Interfaces;
using MangmentBS.Core.Specifications.Buildings;
using MangmentBS.Core.Specifications.Users;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork _unitOfWork,ITokenService _tokenService, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            tokenService = _tokenService;
            mapper = _mapper;
        }
        public async Task<UserData> LoginAsync(LogInView logInView)
        {
            var users = await unitOfWork.Repository<User, string>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.UserName == logInView.UserName && u.Password == logInView.Password);
            if (user == null) return null;
            if (user.Password != logInView.Password) return null;
            return new UserData
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role,
                IsActive = user.IsActive,
                Token = await tokenService.CreateTokenAsync(user) 
            };
        }
        public async Task<UserData> RegisterAsync(RegisterView registerView)
        {
            if(await UserNameIsExist(registerView.UserName)) return null;
            if(await EmailIsExist(registerView.Email)) return null;
            var users= await unitOfWork.Repository<User, string>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.Id == registerView.Id);
            if (user is not null)
            {
                if (user.UserName !=null) return null;

                var Register = new User
                {
                    Id = registerView.Id,
                    Name = user.Name,
                    CreatedAt= DateTime.Now,
                    IsActive = user.IsActive,
                    Role = user.Role,
                    UserName = registerView.UserName,
                    Password = registerView.Password,
                    Email = registerView.Email,
                    PhoneNumber = registerView.PhoneNumber,
                    Address=registerView.Address
                };
                unitOfWork.Repository<User, string>().Update(Register);
                await unitOfWork.SaveChangesAsync();


                return new UserData
                {
                    Id = Register.Id,
                    Name = user.Name,
                    Role = user.Role,
                    IsActive = user.IsActive,
                    Token = await tokenService.CreateTokenAsync(Register)
                };
            }
            else return null;
        }
        public async Task<bool> UserNameIsExist(string username)
        {
            var users = await unitOfWork.Repository<User, string>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.UserName == username);
            if (user == null) return false;
            return true;
        }
        public async Task<bool> EmailIsExist(string Email)
        {
            var users = await unitOfWork.Repository<User, string>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == Email);
            if (user == null) return false;
            return true;
        }
        public async Task<bool> DeActiveUser(string Id)
        {
            var users = await unitOfWork.Repository<User, string>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.Id == Id);
            if (user is not null)
            {
                var Register = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    CreatedAt = user.CreatedAt,
                    IsActive = false,
                    Role = user.Role,
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address
                };
                unitOfWork.Repository<User, string>().Update(Register);
                var result= await unitOfWork.SaveChangesAsync();
                return result > 0 ? true : false;
            }
            return false;
        }
        public async Task<bool> ActiveUser(string Id)
        {
            var users = await unitOfWork.Repository<User, string>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.Id == Id);
            if (user is not null)
            {
                var Register = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    CreatedAt = user.CreatedAt,
                    IsActive = false,
                    Role = user.Role,
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address
                };
                unitOfWork.Repository<User, string>().Update(Register);
                var result = await unitOfWork.SaveChangesAsync();
                return result > 0 ? true : false;
            }
            return false;
        }
        public async Task<bool> DeleteUser(string Id)
        {
            var users = await unitOfWork.Repository<User, string>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.Id == Id);
            if (user is not null)
            {
                var Register = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    CreatedAt = user.CreatedAt,
                    IsActive = user.IsActive,
                    Role = user.Role,
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address
                };
                unitOfWork.Repository<User, string>().Delete(Register);
                var result = await unitOfWork.SaveChangesAsync();
                return result > 0 ? true : false;
            }
            return false;
        }
        public async Task<bool> EditUserAdmin(EditUserAdmin editUser)
        {
            var users = await unitOfWork.Repository<User, string>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.Id == editUser.Id);
            if (user is not null)
            {
                var Register = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    CreatedAt = user.CreatedAt,
                    IsActive = user.IsActive,
                    Role = editUser.Role,
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address
                };
                unitOfWork.Repository<User, string>().Update(Register);
                var result = await unitOfWork.SaveChangesAsync();
                if (result > 0) return true;   
            }
            return false;
        }
        public async Task<UserData> EditUserProfile(EditUserProfile editUser)
        {
            var users = await unitOfWork.Repository<User, string>().GetAllAsync();
            var user = users.FirstOrDefault(u => u.Id == editUser.Id);
            if (user is not null)
            {
                var Register = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    CreatedAt = user.CreatedAt,
                    IsActive = user.IsActive,
                    Role = user.Role,
                    UserName = user.UserName,
                    Password = editUser.Password?? user.Password,
                    Email = editUser.Email?? user.Email,
                    PhoneNumber = editUser.PhoneNumber ??user.PhoneNumber,
                    Address = editUser.Address ?? user.Address
                };
                unitOfWork.Repository<User, string>().Update(Register);
                var result = await unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    users = await unitOfWork.Repository<User, string>().GetAllAsync();
                    user = users.FirstOrDefault(u => u.Id == editUser.Id);
                    if (user is not null)
                    {
                        return new UserData
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Role = user.Role,
                            IsActive = user.IsActive,
                            Token = await tokenService.CreateTokenAsync(user)
                        };
                    }
                }
            }
            return null;
        }
        public async Task<ErrorResponce> AddUser(AddUserView addUser)
        {
            if (addUser.Id == null) return new ErrorResponce("400", "يرجي ادخال رقم المستخدم");
            var user = await unitOfWork.Repository<User, string>().GetByIdAsync(new UserSpecifications(addUser.Id));
            if (user is not null) return new ErrorResponce("400", "رقم المستخدم موجود بالفعل");
            await unitOfWork.Repository<User, string>().AddAsync(new User()
            {
                Id = addUser.Id,
                Name = addUser.Name,
                Role = addUser.Role,
                IsActive = addUser.IsActive,
            });
            var result = await unitOfWork.SaveChangesAsync();
            if(result > 0) return new ErrorResponce("200", "تم الاضافه بنجاح");
            return new ErrorResponce("400", "حدث خطأ اثناء الاضافه, يرجي المحاوله مره اخري");
        }
        public async Task<PaginationResponse<UserTableView>> GetAllUserTableViewAsync(UserSpecificationsParams Params)
        {
            var user = await unitOfWork.Repository<User, string>().GetAllAsync(new UserSpecifications(Params));
            var count = await unitOfWork.Repository<User, string>().CountAsync(new UserSpecifications(Params));
            var maped = mapper.Map<IEnumerable<UserTableView>>(user);
            return new PaginationResponse<UserTableView>(Params.PageSize, Params.PageIndex, count, maped);
        }
    }
}
