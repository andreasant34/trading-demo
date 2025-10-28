using MassTransit;
using Trading.Core.Interfaces.MessageBus;

namespace Trading.Infrastructure.MessageBus
{
    /// <summary>
    /// Implements <see cref="IMessageBus"/>, using the message broker (RabbitMq) registered with MassTransit
    /// </summary>
    public class TradingMessageBus : IMessageBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public TradingMessageBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<T>(T message) where T : class
        {
            await _publishEndpoint.Publish(message);
        }
    }
}