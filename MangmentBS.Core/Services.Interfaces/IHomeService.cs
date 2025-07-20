using MangmentBS.Core.Dtos.Home;
using MangmentBS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Services.Interfaces
{
    public interface IHomeService
    {
        Task<HomeDetails> GetHomeDetails();
        Task<AgendaView> GetAgenda(int Month,int Year);
    }
}
