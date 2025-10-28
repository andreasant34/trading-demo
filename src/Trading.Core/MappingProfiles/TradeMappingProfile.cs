using AutoMapper;
using Trading.Core.Commands;
using Trading.Core.Entities;
using Trading.Core.Models;

namespace Trading.Core.MappingProfiles
{
    public class TradeMappingProfile : Profile
    {
        public TradeMappingProfile()
        {
            _ = CreateMap<TradeEntity, TradeDetails>()
                .ForMember(target => target.Id, target => target.MapFrom(source => source.Id))
                .ForMember(target => target.UserId, target => target.MapFrom(source => source.UserId))
                .ForMember(target => target.InvestmentAccountId, target => target.MapFrom(source => source.InvestmentAccountId))
                .ForMember(target => target.TransactionType, target => target.MapFrom(source => source.TransactionType))
                .ForMember(target => target.SecurityId, target => target.MapFrom(source => source.SecurityId))
                .ForMember(target => target.Quantity, target => target.MapFrom(source => source.Quantity))
                .ForMember(target => target.CurrencyCode, target => target.MapFrom(source => source.CurrencyCode))
                .ForMember(target => target.Price, target => target.MapFrom(source => source.Price))
                .ForMember(target => target.TotalAmount, target => target.MapFrom(source => source.TotalAmount))
                .ForMember(target => target.InvestmentAccountName, target => target.MapFrom(source => source.InvestmentAccount.Name))
                .ForMember(target => target.SecurityName, target => target.MapFrom(source => source.Security.Name))
                .ReverseMap();

            _ = CreateMap<CreateTradeCommand, TradeEntity>()
                .ForMember(target => target.Id, opt => opt.Ignore())
                .ForMember(target => target.UserId, opt => opt.Ignore())
                .ForMember(target => target.InvestmentAccountId, target => target.MapFrom(source => source.InvestmentAccountId))
                .ForMember(target => target.TransactionType, target => target.MapFrom(source => source.TransactionType))
                .ForMember(target => target.SecurityId, target => target.MapFrom(source => source.SecurityId))
                .ForMember(target => target.Quantity, target => target.MapFrom(source => source.Quantity))
                .ForMember(target => target.CurrencyCode, target => target.MapFrom(source => source.CurrencyCode.ToUpper()))
                .ForMember(target => target.Price, target => target.MapFrom(source => source.Price))
                .ForMember(target => target.TotalAmount, target => target.MapFrom(source => source.TotalAmount))
                .ForMember(target => target.User, opt => opt.Ignore())
                .ForMember(target => target.Security, opt => opt.Ignore())
                .ForMember(target => target.InvestmentAccount, opt => opt.Ignore())
                .ReverseMap();

            _ = CreateMap<TradeEntity, TradeCreatedCommand>()
                .ForMember(target => target.Id, target => target.MapFrom(source => source.Id))
                .ForMember(target => target.UserId, target => target.MapFrom(source => source.UserId))
                .ForMember(target => target.InvestmentAccountId, target => target.MapFrom(source => source.InvestmentAccountId))
                .ForMember(target => target.TransactionType, target => target.MapFrom(source => source.TransactionType))
                .ForMember(target => target.SecurityId, target => target.MapFrom(source => source.SecurityId))
                .ForMember(target => target.Quantity, target => target.MapFrom(source => source.Quantity))
                .ForMember(target => target.CurrencyCode, target => target.MapFrom(source => source.CurrencyCode))
                .ForMember(target => target.Price, target => target.MapFrom(source => source.Price))
                .ForMember(target => target.TotalAmount, target => target.MapFrom(source => source.TotalAmount))
                .ReverseMap();

        }
    }
}
