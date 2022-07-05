using API.ApplicationBuilder;
using API.Middelware;
using Core.Entities.Identity;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbInit(builder.Configuration);
builder.Services.AddRegisterDependencyInjection();
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

//redis
builder.Services.AddSingleton<IConnectionMultiplexer>(c => {
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetSection("ConnectionStrings:Redis").Value,
    true);

    var connection = ConnectionMultiplexer.Connect(configuration);
    return connection;
});

builder.Services.AddCors(opt =>
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
    })
);
builder.Services.AddErrorConfig();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

using var scop = app.Services.CreateScope();
var services = scop.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
    await ApplicationContextSeed.SeedAsync(dbContext, loggerFactory);

    var userManagment = services.GetRequiredService<UserManager<AppUser>>();
    var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
    await identityDbContext.Database.MigrateAsync();
    await AppIdentityDbContextSeed.SeedUserAsync(userManagment);
}
catch (Exception ex)
{
    var _logger = loggerFactory.CreateLogger<Program>();
    _logger.LogError(ex, "An error occured during migration");
}

app.UseMiddleware<ExceptionMiddelware>();
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseStatusCodePagesWithReExecute("error/{0}");

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();



