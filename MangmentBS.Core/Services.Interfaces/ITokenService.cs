using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user);
        bool ValidateToken(string token);
    }
}
