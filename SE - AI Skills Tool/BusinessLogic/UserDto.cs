using SE_AI_Skills_Tool.Models;

namespace SE_AI_Skills_Tool.BusinessLogic
{

    public class UserDto
    {
    
    }

    public class AddCoursesToUserDto
    {
        public string UserId { get; set; }
        public Course[] Courses { get; set; }
    }
}