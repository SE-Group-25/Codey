using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE_AI_Skills_Tool.BusinessLogic;
using SE_AI_Skills_Tool.Context;
using SE_AI_Skills_Tool.Models;

namespace SE_AI_Skills_Tool.Services
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(UserDto newUser);

        Task<string> AddCoursesToUserAsync(AddCoursesToUserDto coursesToUser);

        Task<List<Course>?> GetUserCoursesAsync(UserDto user);
    }
    public class UserService : IUserService
    {
        private readonly AstDevContext _astDev;

        public UserService(AstDevContext astDev)
        {
            _astDev = astDev;
        }

        public async Task<string> CreateUserAsync(UserDto user)
        {
            try
            {
                User newUser = new User(user.Id);
                if (!(await _astDev.Users.AnyAsync(u => u.Id == newUser.Id)))
                {
                    await _astDev.Users.AddAsync(newUser);
                    await _astDev.SaveChangesAsync();
                }
                else
                {
                    return "Conflict";
                }

                return "Success";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        // ToDo: FIX THIS SHIT! NO FUCKING CLUE WHAT HAS GONE ON HERE.
        
        public async Task<string> AddCoursesToUserAsync(AddCoursesToUserDto coursesToUser)
        {
            try
            {
                var user = await _astDev.Users.Include(u => u.UserCourses).ThenInclude(uc => uc.Course).Where(u => u.Id == coursesToUser.UserId).SingleOrDefaultAsync();

                if (user != null)
                {
                    if (user.UserCourses == null)
                    {
                        user.UserCourses = new List<UserCourse>();
                    }

                    var course = coursesToUser.Course;
                    if (!user.UserCourses.Any(uc => uc.CourseId == course.Id))
                    {
                        var userCourse = new UserCourse
                                         {
                                             UserId = coursesToUser.UserId,
                                             CourseId = course.Id
                                         };

                        user.UserCourses.Add(userCourse);
                        await _astDev.SaveChangesAsync();
                        return "Success";
                    }
                    
                    //user.UserCourses = user.UserCourses.Concat(coursesToUser.Courses).ToList();
                    // await _astDev.SaveChangesAsync();
                }
                return "Fail";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<Course>?> GetUserCoursesAsync(UserDto user)
        {
            // var userItem = await _astDev.Users.Where(c => c.Id == user.Id).Include(u => u.Courses).FirstOrDefaultAsync();
            // var userItem = await _astDev.Users.Include(u => u.UserCourses).ThenInclude(uc => uc.Course).FirstOrDefaultAsync(u => u.Id == user.Id);
            var userItem = await _astDev.UserCourses.Include(uc => uc.Course).Where(uc => uc.UserId == user.Id).Select(uc => uc.Course).ToListAsync();
            return userItem;
        }
    }
}