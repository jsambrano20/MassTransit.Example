using MassTransit;

namespace MassTransitExample
{
    public class MessageProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MessageProducer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendMessageAsync(string text)
        {
            await _publishEndpoint.Publish(new MyMessage { Text = text });
        }
    }

    public record MyMessage
    {
        public string Text { get; init; } = string.Empty;
    }
}