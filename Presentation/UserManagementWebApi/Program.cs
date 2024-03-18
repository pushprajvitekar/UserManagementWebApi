
using FluentValidation;
using FluentValidation.AspNetCore;
using JwtTokenProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using System.Text.Json.Serialization;
using UserManagement.Application.Contracts;
using UserManagement.Application.Users.Dtos;
using UserManagement.Domain.Users;
using UserManagement.EFCorePersistence;
using UserManagement.WebApi.Auth.Policies;
using UserManagement.WebApi.Extensions;
using UserManagement.WebApi.Filters;

namespace UserManagement.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //configure logging
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog();
            });

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy(PolicyName.SameUserPolicy, policy => policy.Requirements.Add(new SameUserAuthorizationRequirement()));
            builder.Services.AddSingleton<IAuthorizationHandler, SameUserAuthorizationHandler>();

           

            builder.Services.AddControllers(o => o.Filters.Add(typeof(ExceptionFilter)))
                .AddJsonOptions(opt =>
                {// Support string to enum conversions
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<UserDto>();

            // Specify identity requirements
            // Must be added before .AddAuthentication otherwise a 404 is thrown on authorized endpoints
            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserManagementDbContext>()
            ;

            var tokenProviderOptions = builder.Services.CreateTokenProviderOptions(builder.Configuration);
            builder.Services.AddAuthenticationJwtBearer(tokenProviderOptions);
            builder.Services.AddScoped<ITokenProvider, TokenProvider>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter a valid JSON web token here",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });

            builder.Services.AddMediator();
            builder.Services.RegisterDBContext(builder.Configuration);
            builder.Services.AddRepositories();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
