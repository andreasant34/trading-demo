using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Trading.Core.Behaviors;
using Trading.Core.Validators;

namespace Trading.Core.Extensions
{
    public static class CoreServiceCollectionExtensions
    {
        public static IServiceCollection AddTradingCoreServices(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(x => x.AddMaps(Assembly.GetExecutingAssembly()));
            _ = services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            // _ = services.AddValidatorsFromAssemblyContaining<CreateTradeCommandValidator>();
            _ = services.AddTransient<IValidator, CreateTradeCommandValidator>();
            _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
