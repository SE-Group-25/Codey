using Microsoft.AspNetCore.Mvc;
using SE_AI_Skills_Tool.Services;

namespace SE_AI_Skills_Tool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController: ControllerBase
    {
        
        
        public Watson chat = new Watson();

        
        
        private static readonly string[] Summaries = new[]
                                                     {
                                                         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
                                                     };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                                                          {
                                                              Date = DateTime.Now.AddDays(index),
                                                              TemperatureC = Random.Shared.Next(-20, 55),
                                                              Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                                                          })
                             .ToArray();
        }

        // [HttpGet("Message")]
        // public void SendMessage()
        // {
        //     Watson chat = new Watson();
        //     chat.CreateSession();
        //     chat.Message();
        //     chat.DeleteSession();
        //
        //     Console.WriteLine("DONE!!!");
        // }

        [HttpGet("Analyze")]
        public void AnalyzeMessage()
        {
            WatsonNLUService example = new WatsonNLUService();

            example.Analyze();

            Console.WriteLine("Example Complete");
        }

        
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
