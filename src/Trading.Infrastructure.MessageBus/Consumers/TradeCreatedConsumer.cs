using MassTransit;
using MediatR;
using Trading.Core.Commands;

namespace Trading.Infrastructure.MessageBus.Consumers
{
    /// <summary>
    /// The MassTransit consumer of a <see cref="TradeCreatedCommand"/>
    /// </summary>
    public class TradeCreatedConsumer : IConsumer<TradeCreatedCommand>
    {
        private readonly IMediator _mediator;

        public TradeCreatedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<TradeCreatedCommand> context)
        {
            var message = context.Message;
            await _mediator.Send(message);
        }
    }
}
