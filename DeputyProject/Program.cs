using Application;
using Application.Contracts.Repos;
using DeputyProject.Common;
using Infrastructure;
using Infrastructure.Dbcontext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "أدخل الـ Access Token فقط بدون كلمة Bearer."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddAuthorization();

Console.WriteLine($"[DEBUG] Jwt:Key = '{builder.Configuration["Jwt:Key"]}'");

builder.Services.AddInfrastructure(builder.Configuration).AddApplication();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
        // NOTE: no .AllowCredentials() needed if you're just sending
        // the JWT in a header (not using cookies)
    });
});

builder.Services
              .AddIdentityCore<Domain.User>(options =>
              {
                  // Password settings
                  options.Password.RequiredLength = 8;
                  options.Password.RequireDigit = true;
                  options.Password.RequireUppercase = true;
                  options.Password.RequireLowercase = true;
                  options.Password.RequireNonAlphanumeric = false;

                  // User settings
                  options.User.RequireUniqueEmail = true;

                  // Lockout settings
                  options.Lockout.MaxFailedAccessAttempts = 5;
                  options.Lockout.DefaultLockoutTimeSpan =
                      TimeSpan.FromMinutes(5);
              })
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<Appcontext>()
              .AddDefaultTokenProviders();
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
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.    UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
