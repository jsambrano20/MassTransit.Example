using Microsoft.AspNetCore.Mvc;

namespace MassTransitExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageProducer _messageProducer;

        public MessageController(MessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] string text)
        {
            await _messageProducer.SendMessageAsync(text);
            return Ok("Mensagem enviada com sucesso!");
        }
    }
}
