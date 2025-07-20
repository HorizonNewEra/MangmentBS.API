using AutoMapper;
using MangmentBS.Core.Dtos.Building;
using MangmentBS.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Mapping.Buildings
{
    public class BuildingProfile :Profile
    {
        public BuildingProfile(IConfiguration configuration)
        {
            CreateMap<Building, BuildingTableView>()
                .ForMember(dest => dest.FlatCount, opt => opt.MapFrom(src => src.Flats.Count()))
                .ForMember(dest => dest.LandContractImages, opt => opt.MapFrom(src => src.BuildingContractImages.Select(p => $"{configuration["BaseUrl"]}/{p.ImageLink}")));
        }
    }
}
