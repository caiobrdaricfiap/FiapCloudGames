using FiapCloudGames.Infrastructure.Repositories;
using FiapCloudGamesWebAPI.Application.Services;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Infrastructure.Repositories;

namespace FiapCloudGameWebAPI.IoC
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddServices();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGamesLibraryRepository, GamesLibraryRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<AuthService>();
            services.AddScoped<GameService>();
            services.AddScoped<GamesLibraryService>();
            return services;
        }
    }

}
