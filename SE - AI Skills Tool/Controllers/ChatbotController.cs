// using Microsoft.AspNetCore.Mvc;
// using SE_AI_Skills_Tool.BusinessLogic;
// using SE_AI_Skills_Tool.Services;
//
//
// namespace SE_AI_Skills_Tool.Controllers
// {
//     [ApiController]
//     // [Route("chatbot")]
//     [Route("[controller]")]
//     public class ChatbotController: ControllerBase
//     {
//         private string? sessionId;
//         private readonly IWatson _watson;
//
//         public ChatbotController(IWatson watson)
//         {
//             _watson = watson;
//         }
//             
//         [HttpGet("StartChat")]
//         public void StartChat()
//         {
//             _watson.CreateSession();
//         }
//
//         [HttpPost("Message")]
//         public MessageResponseDto Message(string msgString)
//         {
//             if (sessionId == null)
//             {
//                 sessionId = _watson.CreateSession();
//             }
//             return _watson.Message(msgString, sessionId);
//         }
//
//         // ToDo: Write function to close the chat session. 
//     }
// }
