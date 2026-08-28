using Application;
using Application.Contracts.Repos;
using DeputyProject.Common;
using Infrastructure;
using Infrastructure.Dbcontext;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration).AddApplication();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Appcontext>();
    var logger = scope.ServiceProvider
        .GetRequiredService<ILogger<Program>>();

    var canConnect = await context.Database.CanConnectAsync();

    logger.LogInformation(
        "DATABASE CONNECTION: {CanConnect}",
        canConnect);
}
app.UseExceptionHandling();

// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
