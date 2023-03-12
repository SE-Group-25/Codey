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

        Task<Course[]?> GetUserCoursesAsync(UserDto user);
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
                await _astDev.Users.AddAsync(newUser);
                await _astDev.SaveChangesAsync();

                return "Success";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AddCoursesToUserAsync(AddCoursesToUserDto coursesToUser)
        {
            try
            {
                var user = await _astDev.Users.Where(u => u.Id == coursesToUser.UserId).SingleOrDefaultAsync();

                if (user.Courses != null)
                {
                    user.Courses = user.Courses.Concat(coursesToUser.Courses).ToArray();
                    await _astDev.SaveChangesAsync();
                }

                return "Success";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Course[]?> GetUserCoursesAsync(UserDto user)
        {
            var userItem = await _astDev.Users.Where(c => c.Id == user.Id).FirstOrDefaultAsync();
            return userItem?.Courses;
        }
    }
}