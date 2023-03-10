using Microsoft.AspNetCore.Mvc;
using SE_AI_Skills_Tool.Services;
using SE_AI_Skills_Tool.BusinessLogic;


namespace SE_AI_Skills_Tool.Controllers
{
    [ApiController]
    [Route("chatbot")]
    // [Route("[controller]")]
    public class CodeyController: ControllerBase
    {
        private readonly IWatson _watson;

        public CodeyController(IWatson watson)
        {
            _watson = watson;
        }
            
        [HttpGet("StartChat")]
        public void StartChat()
        {
            _watson.CreateSession();
        }

        [HttpPost("Message")]
        public MessageResponseDto Message(SendMessageDto sendMessageDto)
        {
            if (sendMessageDto.SessionId == null)
            {
                sendMessageDto.SessionId = _watson.CreateSession();
            }
            return _watson.Message(sendMessageDto.MsgString, sendMessageDto.SessionId);
        }

        // ToDo: Write function to close the chat session. 
    }
}
