using SE_AI_Skills_Tool.Models;

namespace SE_AI_Skills_Tool.BusinessLogic
{

    public class UserDto
    {
        public string Id { get; set; } //????? maybe right?
    }

    public class AddCoursesToUserDto
    {
        public string UserId { get; set; }
        public Course Course { get; set; }
    }
}