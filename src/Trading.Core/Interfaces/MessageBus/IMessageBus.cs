namespace Trading.Core.Interfaces.MessageBus
{
    public interface IMessageBus
    {
        Task PublishAsync<T>(T message) where T : class;
    }
}