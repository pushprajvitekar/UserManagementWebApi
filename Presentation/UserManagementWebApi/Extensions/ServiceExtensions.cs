
using JwtTokenProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagement.Application.Contracts;
using UserManagement.Application.Users.Dtos;
using UserManagement.EFCorePersistence;
namespace UserManagement.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration["Data:DefaultConnection:ConnectionString"] ?? throw new ArgumentNullException(nameof(configuration), $"ConnectionString not defined");
            services.AddDbContext<UserManagementDbContext>(options => options.UseSqlServer(connString));
        }

        public static void AddAuthenticationJwtBearer(this IServiceCollection services, TokenProviderOptions tokenProviderOptions)
        {
           
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenProviderOptions.ValidIssuer,
                        ValidAudience = tokenProviderOptions.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(tokenProviderOptions.SymmetricSecurityKey)

                    };
                });

        }

        public  static TokenProviderOptions CreateTokenProviderOptions(this IServiceCollection services, IConfiguration configuration)
        {
            // These will eventually be moved to a secrets file, but for alpha development appsettings is fine
            var validIssuer = configuration["Data:Jwt:Issuer"] ?? throw new ArgumentNullException(nameof(configuration), $"Jwt Issuer not defined");
            var validAudience = configuration["Data:Jwt:Audience"] ?? throw new ArgumentNullException(nameof(configuration), $"Jwt Audience not defined");
            var symmetricSecurityKey = Encoding.UTF8.GetBytes(configuration["Data:Jwt:Key"] ?? throw new ArgumentNullException(nameof(configuration), $"Jwt Key not defined"));
            TokenProviderOptions options = new TokenProviderOptions(validIssuer, validAudience, symmetricSecurityKey);
            services.AddSingleton(options);
            return options;
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(UserDto).Assembly);

            });
        }
    }
}
