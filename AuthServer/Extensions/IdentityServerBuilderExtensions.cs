using AuthServer.Service;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServer.Extensions
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.AddProfileService<AuthUserProfileService>();

            return builder;
        }
    }
}
