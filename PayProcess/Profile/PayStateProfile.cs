using PayProcess.Models.DTO;
using PayProcess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayProcess.Services.Profile
{
    public class PayStateProfile : AutoMapper.Profile
    {
        public PayStateProfile()
        {
            CreateMap<PayStateDto, PaymentState>()
                .ForMember(dest => dest.Payment, opt => opt.Ignore())
                .ForMember(dest => dest.PayId, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentStateId, opt => opt.Ignore())
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.PayState.ToString()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.PaymentStateDate))
                .ReverseMap()
                ;
        }
    }
}
