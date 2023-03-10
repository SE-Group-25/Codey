namespace SE_AI_Skills_Tool.BusinessLogic
{

    public class SuccessDto
    {
        public bool IsSuccessful { get; set; }
        
        public string? ErrorMessage { get; set; }
        
        public int? ItemsChanged { get; set; }
    }
}