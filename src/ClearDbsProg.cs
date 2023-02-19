using Microsoft.AspNetCore.Identity;
using src.Data;
using src.Entities;

namespace src
{
    public class ClearDbsProg
    {

        public static async Task ClearDbs(IApplicationBuilder app, IConfiguration config)
        {
            using var scope = app?.ApplicationServices.CreateScope();
            AppIdentityDbContext identityDbContext =
            scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await identityDbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureDeletedAsync();
        }
        public static async Task CreateDbs(IApplicationBuilder app, IConfiguration config)
        {
            using var scope = app?.ApplicationServices.CreateScope();
            AppIdentityDbContext identityDbContext =
            scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await identityDbContext.Database.EnsureCreatedAsync();
            await dbContext.Database.EnsureCreatedAsync();
        }
    }
}