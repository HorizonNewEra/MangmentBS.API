using AutoMapper;
using MangmentBS.Core.Dtos.Client;
using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Mapping.Clients
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientTableView>()
                .ForMember(dest => dest.FlatCount, opt => opt.MapFrom(src => src.Flats.Count()));
        }
    }
}
