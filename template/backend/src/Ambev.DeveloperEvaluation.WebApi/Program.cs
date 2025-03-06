using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 123abcde'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TokenValidationMiddleware>();

            app.UseBasicHealthChecks();

            app.MapControllers();

            if (!builder.Configuration.IsUnitTestEnviroment())
                MigrateDatabase(app.Services);

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void MigrateDatabase(IServiceProvider service)
    {
        using (var scope = service.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DefaultContext>();

            context.Database.Migrate();

            AddTestData(context);
        }
    }

    private static void AddTestData(DefaultContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Email = "Admin@taking.com.br",
                Username = "Taking Admin",
                Password = "$2a$11$uVkKp9dH.FHvQeE1sszr.ud.9hYCFPMv58jQaWIkH2or8ArqF4XCG",
                Phone = "+551141026121",
                Status = Domain.Enums.UserStatus.Active,
                Role = Domain.Enums.UserRole.Admin
            });
            context.SaveChanges();

            context.Users.Add(new User
            {
                Email = "Manager@taking.com.br",
                Username = "Taking Manager",
                Password = "$2a$11$uVkKp9dH.FHvQeE1sszr.ud.9hYCFPMv58jQaWIkH2or8ArqF4XCG",
                Phone = "+551141026121",
                Status = Domain.Enums.UserStatus.Active,
                Role = Domain.Enums.UserRole.Manager
            });
            context.SaveChanges();

            context.Users.Add(new User
            {
                Email = "Customer@taking.com.br",
                Username = "Taking Customer",
                Password = "$2a$11$uVkKp9dH.FHvQeE1sszr.ud.9hYCFPMv58jQaWIkH2or8ArqF4XCG",
                Phone = "+551141026121",
                Status = Domain.Enums.UserStatus.Active,
                Role = Domain.Enums.UserRole.Customer
            });
            context.SaveChanges();
        }

        if (!context.Branches.Any())
        {
            context.Branches.Add(new Branch
            {
                Name = "Main store"
            });
            context.SaveChanges();

            context.Branches.Add(new Branch
            {
                Name = "Neighborhood store"
            });
            context.SaveChanges();

            context.Branches.Add(new Branch
            {
                Name = "Downtown store"
            });
            context.SaveChanges();
        }

        if (!context.Products.Any())
        {
            context.Products.Add(new Product
            {
                Title = "Laptop",
                Price = 1500.00m,
                Description = "High performance laptop with 16GB RAM and 512GB SSD.",
                Category = "Electronics",
                Rating = new Rating
                {
                    Rate = 0,
                    Count = 0
                }
            });
            context.SaveChanges();

            context.Products.Add(new Product
            {
                Title = "Smartphone",
                Price = 800.00m,
                Description = "Latest model smartphone with 128GB storage and 5G support.",
                Category = "Electronics",
                Rating = new Rating
                {
                    Rate = 0,
                    Count = 0
                }
            });
            context.SaveChanges();

            context.Products.Add(new Product
            {
                Title = "Wireless Headphones",
                Price = 200.00m,
                Description = "Noise-cancelling wireless headphones with 20 hours battery life.",
                Category = "Accessories",
                Rating = new Rating
                {
                    Rate = 0,
                    Count = 0
                }
            });
            context.SaveChanges();

            context.Products.Add(new Product
            {
                Title = "Smartwatch",
                Price = 250.00m,
                Description = "Smartwatch with heart rate monitor and GPS tracking.",
                Category = "Wearables",
                Rating = new Rating
                {
                    Rate = 0,
                    Count = 0
                }
            });
            context.SaveChanges();

            context.Products.Add(new Product
            {
                Title = "Gaming Console",
                Price = 500.00m,
                Description = "Next-gen gaming console with 1TB storage and 4K support.",
                Category = "Gaming",
                Rating = new Rating
                {
                    Rate = 0,
                    Count = 0
                }
            });
            context.SaveChanges();

            context.Products.Add(new Product
            {
                Title = "Bluetooth Speaker",
                Price = 100.00m,
                Description = "Portable Bluetooth speaker with 10 hours battery life.",
                Category = "Audio",
                Rating = new Rating
                {
                    Rate = 0,
                    Count = 0
                }
            });
            context.SaveChanges();
        }

    }
}
