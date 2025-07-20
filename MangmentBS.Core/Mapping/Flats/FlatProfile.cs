using AutoMapper;
using MangmentBS.Core.Dtos.Flat;
using MangmentBS.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Mapping.Flats
{
    public class FlatProfile : Profile
    {
        public FlatProfile(IConfiguration configuration)
        {
            CreateMap<Flat, FlatTableView>()
                    .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(src => src.Building.Name))
                    .ForMember(dest => dest.FlatContractImages, opt => opt.MapFrom(src => src.FlatContractImages.Select(p => $"{configuration["BaseUrl"]}/{p.ImageLink}")))
                    .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name));
        }
    }
}
