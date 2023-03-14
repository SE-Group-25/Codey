using SE_AI_Skills_Tool.Services;

namespace SE_AI_Skills_Tool.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterRepos(this IServiceCollection collection)
        {
            collection.AddTransient<IWatson, Watson>();
            collection.AddTransient<IWatsonNLUService, WatsonNLUService>();
            collection.AddTransient<IUserService, UserService>();
            collection.AddTransient<ICourseService, CourseService>();
        }
    }
}
