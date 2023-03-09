using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SE_AI_Skills_Tool.Context;
using SE_AI_Skills_Tool.Models;

namespace SE_AI_Skills_Tool.Services
{
    public interface IUserService
    {
        Task<string> CreateUser(User newUser);

        Task<string> AddCoursesToUser(Course[] courses, string userId);

        Task<Course[]?> GetUserCourses(User user);
    }
    public class UserService : IUserService
    {
        private readonly AstDevContext _astDev;

        public UserService(AstDevContext astDev)
        {
            _astDev = astDev;
        }

        public async Task<string> CreateUser(User newUser)
        {
            try
            {
                await _astDev.Users.AddAsync(newUser);
                await _astDev.SaveChangesAsync();

                return "Success";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AddCoursesToUser(Course[] courses, string userId)
        {
            try
            {
                var user = await _astDev.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();

                if (user.Courses != null)
                {
                    user.Courses = user.Courses.Concat(courses).ToArray();
                    await _astDev.SaveChangesAsync();
                }

                return "Success";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Course[]?> GetUserCourses(User user)
        {
            var userItem = await _astDev.Users.Where(c => c.Id == user.Id).FirstOrDefaultAsync();
            return userItem?.Courses;
        }
    }
}