using Microsoft.AspNetCore.Mvc;
using SE_AI_Skills_Tool.BusinessLogic;
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
        public async Task<IActionResult> AddCourses(Course[] courses)
        {
            int count = 0;
            try
            {
                foreach(Course course in courses)
                {
                    await _courseService.AddCourseAsync(course);
                    count++;
                }

                return Ok(new SuccessDto { IsSuccessful = true, ItemsChanged = count });
            }
            catch(Exception ex)
            {
                return BadRequest(new SuccessDto { IsSuccessful = false, ErrorMessage = ex.Message, ItemsChanged = count });
            }
        }

        [HttpPost("GetCourses")]
        public async Task<List<Course>> GetCourses(string[] courseList)
        {
            return await _courseService.GetCoursesAsync(courseList);
        }
    }
}