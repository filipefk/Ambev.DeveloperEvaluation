using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestUtil.Entities;

namespace Ambev.DeveloperEvaluation.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{

    public Dictionary<string, object> GetEntitiesCreated()
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
        dbContext.Database.EnsureDeleted();

        var entities = StartDatabase(dbContext);

        return entities;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DefaultContext>)
                );

                if (descriptor is not null)
                    services.Remove(descriptor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<DefaultContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

            });
    }

    private Dictionary<string, object> StartDatabase(DefaultContext dbContext)
    {
        var entities = new Dictionary<string, object>();

        var passwordEncriptor = new BCryptPasswordHasher();

        var activeUserCreated = UserBuilder.GenerateValidUser();
        activeUserCreated.Status = Domain.Enums.UserStatus.Active;
        activeUserCreated.Role = Domain.Enums.UserRole.Admin;
        var activeUserPassworNotEncripted = activeUserCreated.Password;
        activeUserCreated.Password = passwordEncriptor.HashPassword(activeUserCreated.Password);
        dbContext.Users.Add(activeUserCreated);
        dbContext.SaveChanges();
        entities["ActiveUser"] = activeUserCreated;

        var inactiveUserCreated = UserBuilder.GenerateValidUser();
        inactiveUserCreated.Status = Domain.Enums.UserStatus.Inactive;
        var inactiveUserPassworNotEncripted = inactiveUserCreated.Password;
        inactiveUserCreated.Password = passwordEncriptor.HashPassword(inactiveUserCreated.Password);
        dbContext.Users.Add(inactiveUserCreated);
        dbContext.SaveChanges();
        entities["InactiveUser"] = inactiveUserCreated;

        var product = ProductBuilder.Build();
        dbContext.Products.Add(product);
        dbContext.SaveChanges();
        entities["Product"] = product;

        activeUserCreated.Password = activeUserPassworNotEncripted;
        inactiveUserCreated.Password = inactiveUserPassworNotEncripted;
        return entities;
    }
}
