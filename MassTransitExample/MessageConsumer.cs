using MassTransit;

namespace MassTransitExample
{
    public class MyMessageConsumer : IConsumer<MyMessage>
    {
        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            Console.WriteLine($"Mensagem recebida: {context.Message.Text}");
        }
    }
}