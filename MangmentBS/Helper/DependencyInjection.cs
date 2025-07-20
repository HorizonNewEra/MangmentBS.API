using MangmentBS.API.Errors;
using MangmentBS.Core;
using MangmentBS.Core.Mapping.Buildings;
using MangmentBS.Core.Mapping.Clients;
using MangmentBS.Core.Mapping.Flats;
using MangmentBS.Core.Mapping.Payments;
using MangmentBS.Core.Mapping.Users;
using MangmentBS.Core.Services.Interfaces;
using MangmentBS.Repository;
using MangmentBS.Repository.Data.Contexts;
using MangmentBS.Services.Services.BuildingServices;
using MangmentBS.Services.Services.ClientServices;
using MangmentBS.Services.Services.FlatServices;
using MangmentBS.Services.Services.HomeServices;
using MangmentBS.Services.Services.InstallmentServices;
using MangmentBS.Services.Services.PaymentServices;
using MangmentBS.Services.Services.Tokens;
using MangmentBS.Services.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MangmentBS.API.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuildInService();
            services.AddSwaggerServices();
            services.AddDbContextServices(configuration);
            services.AddUserServices();
            return services;
        }
        private static IServiceCollection AddBuildInService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("SQL"));
            });
            services.AddAutoMapper(m => m.AddProfile(new BuildingProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new FlatProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new ClientProfile()));
            services.AddAutoMapper(m => m.AddProfile(new PaymentProfile()));
            services.AddAutoMapper(m => m.AddProfile(new UserProfile()));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))

                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    builder =>
                    {
                        //.AllowAnyOrigin()
                        //WithOrigins(configuration["AngularAppUrlhttp"], configuration["AngularAppUrlhttps"])
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                               //.AllowCredentials();
                    });
            });
            return services;
        }
        private static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenServices>();
            services.AddScoped<IFlatService, FlatService>();
            services.AddScoped<IClientServices, ClientService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IInstallmentService, InstallmentService>();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return new BadRequestObjectResult(new ApiValidationError(errors));
                };
            });
            return services;
        }
    }
}
