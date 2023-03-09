using Microsoft.AspNetCore.Mvc;
using SE_AI_Skills_Tool.Models;
using SE_AI_Skills_Tool.Services;

namespace SE_AI_Skills_Tool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("AddCourses")]
        public async Task<int> AddCourses(Course[] courses)
        {
            int count = 0;
            foreach(Course course in courses)
            {
                await _courseService.AddCourse(course);
                count++;
            }

            return count;
        }
    }
}