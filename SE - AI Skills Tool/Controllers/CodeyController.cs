using Microsoft.AspNetCore.Mvc;
using SE_AI_Skills_Tool.Services;


namespace SE_AI_Skills_Tool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeyController: ControllerBase
    {
        public Watson chat = new Watson();

        [HttpGet("StartChat")]
        public void StartChat()
        {
            chat.CreateSession();
        }

        [HttpGet("Message")]
        public string Message(string msgString)
        {
            return chat.Message(msgString);
        }
        
        [HttpGet("CloseChat")]
        public void CloseChat()
        {
            chat.DeleteSession();
        }
    }
}
