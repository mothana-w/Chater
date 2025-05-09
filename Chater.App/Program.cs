using System.Text;
using Chater.App.Services;
using Chater.Data.Model;
using Chater.Data.Repository;
using Chater.Options;
using Chater.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSignalR();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json")
    .AddJsonFile($"appsettings.{environment}.json")
    .Build();
var constr = configuration.GetConnectionString("Default");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// configuration options
builder.Services.AddOptions<JwtOptions>()
    .Bind(configuration.GetSection("Jwt"))
    .ValidateDataAnnotations()
    .ValidateOnStart();
builder.Services.AddOptions<UserOptions>()
    .Bind(configuration.GetSection("User"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IServiceResultFactory, ServiceResultFactory>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IChatRoomService, ChatRoomService>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opts => {
    opts.UseNpgsql(constr);
});
var jwtOpts = configuration.GetSection("Jwt").Get<JwtOptions>()!;
builder.Services.AddAuthentication()
.AddJwtBearer(BearerTokenDefaults.AuthenticationScheme, opts => {
    opts.SaveToken = true;
    opts.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtOpts.Issuer,
        ValidAudience = jwtOpts.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpts.SigningKey))
    };
    opts.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken))// && StartsWithSegments(<>)
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors();
app.UseExceptionHandler(errApp => {
    errApp.Run(async ctx => {
        ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
        ctx.Response.ContentType = "applications/json";

        var exception = ctx.Features.Get<IExceptionHandlerPathFeature>()?.Error;

        var logger = ctx.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "Unhandled Exception Occurred");

        await ctx.Response.WriteAsJsonAsync("Something went wrong processing your request, please try again later.");
    });
});

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
