using ApiAspNet.Helpers;
using ApiAspNet.Models.Auth;
using ApiAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Services
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("WebApiDB")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("WebApiDB")));

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFlotteService, FlotteService>();
builder.Services.AddScoped<IVoyageService, VoyageService>();
builder.Services.AddScoped<IAgenceService, AgenceService>();
builder.Services.AddScoped<IChauffeurService, ChauffeurService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IGestionnaireService, GestionnaireService>();
builder.Services.AddScoped<IOffreService, OffreService>();

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Authentication JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = configuration["JWT:Authority"];
    options.SaveToken = true;
    options.RequireHttpsMetadata = configuration.GetValue<bool>("JWT:RequireHttpsMetadata");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidAudiences = configuration.GetSection("JWT:ValidAudiences").Get<string[]>()
    };
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapGet("users/me", (ClaimsPrincipal claimsPrincipal) => {
    return claimsPrincipal.Claims.ToDictionary(c => c.Type, c => c.Value);
}).RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
