namespace SE_AI_Skills_Tool.BusinessLogic
{

    public class SendMessageDto
    {
        public string MsgString { get; set; }
        
        public string? SessionId { get; set; }
    }

    public class MessageResponseDto
    {
        public string ResponseString { get; set; }
        
        public bool IsSuccessful { get; set; }
    }
}
