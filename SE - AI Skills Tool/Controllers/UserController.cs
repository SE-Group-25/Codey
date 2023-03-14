using Microsoft.AspNetCore.Mvc;
using SE_AI_Skills_Tool.BusinessLogic;
using SE_AI_Skills_Tool.Models;
using SE_AI_Skills_Tool.Services;

namespace SE_AI_Skills_Tool.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            var result = await _userService.CreateUserAsync(user);
            if (result == "Success")
            {
                return Ok(new SuccessDto {IsSuccessful = true});
            }
            else
            {
                return BadRequest(new SuccessDto { IsSuccessful = false, ErrorMessage = result});
            }
        }

        [HttpPost("AddCoursesToUser")]
        public async Task<IActionResult> AddCoursesToUser(AddCoursesToUserDto coursesToUser)
        {
            var result = await _userService.AddCoursesToUserAsync(coursesToUser);
            if (result == "Success")
            {
                return Ok(new SuccessDto { IsSuccessful = true });
            }
            else
            {
                return BadRequest(new SuccessDto { IsSuccessful = false, ErrorMessage = result });
            }
        }

        [HttpPost("GetUserCourses")]
        public async Task<List<Course>?> GetUserCourses(UserDto user)
        {
            return await _userService.GetUserCoursesAsync(user);
        }
    }
}