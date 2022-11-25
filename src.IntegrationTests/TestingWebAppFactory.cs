using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using src.Data;

public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => 
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppIdentityDbContext>)
            );

            var descriptor2 = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
            );

            if(descriptor != null)
                services.Remove(descriptor);

            if(descriptor2 != null)
                services.Remove(descriptor2);

            services.AddDbContext<AppIdentityDbContext>(options => 
            {
                options.UseInMemoryDatabase("InMemoryInfoTest");
            });

            services.AddDbContext<ApplicationDbContext>(options => 
            {
                options.UseInMemoryDatabase("InMemoryInfoTest");
            });

            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                try
                {
                    appContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            using (var scope = sp.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>())
            {
                try
                {
                    appContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        });
    }
}