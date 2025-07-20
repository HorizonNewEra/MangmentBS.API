using AutoMapper;
using MangmentBS.Core.Dtos.Payment;
using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Mapping.Payments
{
    public class PaymentProfile :Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentTableView>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                .ForMember(dest => dest.FlatNumber, opt => opt.MapFrom(src => src.Flat.FlatNumber))
                .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(src => src.Building.Name));
        }
    }
}
