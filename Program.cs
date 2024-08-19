using Services;
using Context;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/logs.txt")
    .CreateLogger();

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT_ISSUER"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT"])),
            NameClaimType = ClaimTypes.Name
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                Log.Logger.Information($"Received JWT: {token}");
                return Task.CompletedTask;
            },

            OnAuthenticationFailed = context =>
            {
                Log.Logger.Error($"Authentication failed: {context.Exception}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Log.Logger.Warning($"Authorization challenge: {context.AuthenticateFailure} {context.Error} {context.ErrorDescription} {context.ErrorUri}");
                return Task.CompletedTask;
            }
        };
    });



var AllowedOriginPolicy = "_allowedOriginPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowedOriginPolicy,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<FlashcardService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<StudySetService>();
builder.Services.AddScoped<IRepository, Repo>();


builder.Services.AddDbContext<FlashcardContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowedOriginPolicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
