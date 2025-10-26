using AutoMapper;
using Trading.API.Commands;
using Trading.API.Queries;
using Trading.Core.Models;

namespace Trading.API.MappingProfiles
{
    public class TradeDtoMappingProfile : Profile
    {
        public TradeDtoMappingProfile()
        {
            _ = CreateMap<TradeDetails, ListTradesDto>()
                .ForMember(target => target.Id, target => target.MapFrom(source => source.Id))
                .ForMember(target => target.UserId, target => target.MapFrom(source => source.UserId))
                .ForMember(target => target.InvestmentAccountId, target => target.MapFrom(source => source.InvestmentAccountId))
                .ForMember(target => target.TransactionType, target => target.MapFrom(source => source.TransactionType))
                .ForMember(target => target.SecurityId, target => target.MapFrom(source => source.SecurityId))
                .ForMember(target => target.Quantity, target => target.MapFrom(source => source.Quantity))
                .ForMember(target => target.CurrencyCode, target => target.MapFrom(source => source.CurrencyCode))
                .ForMember(target => target.Price, target => target.MapFrom(source => source.Price))
                .ForMember(target => target.TotalAmount, target => target.MapFrom(source => source.TotalAmount))
                .ForMember(target => target.InvestmentAccountName, target => target.MapFrom(source => source.InvestmentAccountName))
                .ForMember(target => target.SecurityName, target => target.MapFrom(source => source.SecurityName));

            _ = CreateMap<CreateTradeCommand, TradeCreationDetails>()
                .ForMember(target => target.InvestmentAccountId, target => target.MapFrom(source => source.InvestmentAccountId))
                .ForMember(target => target.TransactionType, target => target.MapFrom(source => source.TransactionType))
                .ForMember(target => target.SecurityId, target => target.MapFrom(source => source.SecurityId))
                .ForMember(target => target.Quantity, target => target.MapFrom(source => source.Quantity))
                .ForMember(target => target.CurrencyCode, target => target.MapFrom(source => source.CurrencyCode))
                .ForMember(target => target.Price, target => target.MapFrom(source => source.Price))
                .ForMember(target => target.TotalAmount, target => target.MapFrom(source => source.TotalAmount));
        }
    }
}
