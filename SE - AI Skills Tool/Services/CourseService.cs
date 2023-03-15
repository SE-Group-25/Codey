using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using SE_AI_Skills_Tool.Context;
using SE_AI_Skills_Tool.Models;

namespace SE_AI_Skills_Tool.Services
{
    public interface ICourseService
    {
        Task<bool> AddCourseAsync(Course course);

        Task<List<Course>> GetCoursesAsync(string[] searchTerm);
    }

    public class CourseService : ICourseService
    {
        private readonly AstDevContext _astDev;

        public CourseService(AstDevContext astDev)
        {
            _astDev = astDev;
        }

        public async Task<bool> AddCourseAsync(Course course)
        {
            try
            {
                await _astDev.Courses.AddAsync(course);
                await _astDev.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Course>> GetCoursesAsync(string[] searchTerm)
        {
            List<Course> CourseList = new List<Course>();
            foreach(string term in searchTerm)
            {
                CourseList = CourseList.Concat(await _astDev.Courses.Where(course => course.Tags.Contains(term)).ToListAsync()).ToList();
            }

            return CourseList;
        }
    }
}