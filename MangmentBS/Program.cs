
using MangmentBS.API.Errors;
using MangmentBS.API.Helper;
using MangmentBS.API.Middlewares;
using MangmentBS.Core;
using MangmentBS.Core.Mapping.Buildings;
using MangmentBS.Core.Services.Interfaces;
using MangmentBS.Repository;
using MangmentBS.Repository.Data;
using MangmentBS.Repository.Data.Contexts;
using MangmentBS.Services.Services.BuildingServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

namespace MangmentBS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependency(builder.Configuration);

            var app = builder.Build();

            await app.UseCustomMiddlewareAsync();

            app.Run();
        }
    }
}
