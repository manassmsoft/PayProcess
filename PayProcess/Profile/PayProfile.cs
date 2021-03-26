using PayProcess.Models.DTO;
using PayProcess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayProcess.Services.Profile
{
    public class PayProfile : AutoMapper.Profile
    {
        public PayProfile()
        {
            CreateMap<PayRequestDto, Payment>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.CardHolder, opt => opt.MapFrom(src => src.CardHolder))
                .ForMember(dest => dest.CreditCardNumber, opt => opt.MapFrom(src => src.CreditCardNumber))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
                .ForMember(dest => dest.PayId, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentStates, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityCode, opt => opt.MapFrom(src => src.SecurityCode))
                .ReverseMap();
                
        }
    }
}
