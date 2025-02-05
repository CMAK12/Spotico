using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Spotico.Domain.Database;
using Spotico.Domain.Stores;
using Spotico.Infrastructure;
using Spotico.Infrastructure.Configuration;
using Spotico.Infrastructure.Interfaces;
using Spotico.Persistence;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
var authOptions = configuration.GetSection("Auth").Get<AuthOptions>();
builder.Services.Configure<AuthOptions>(configuration.GetSection("Auth"));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"))
);

// PostgreSQL
builder.Services.AddDbContext<SpoticoDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("SqlConnection"));
});

// Repositories
builder.Services.AddScoped<IUserStore, UserRepository>();
builder.Services.AddScoped<IPlaylistStore, PlaylistRepository>();
builder.Services.AddScoped<IAlbumStore, AlbumRepository>();
builder.Services.AddScoped<ITrackStore, TrackRepository>();
// Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSingleton<IMediaService, MediaService>();

builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = authOptions.Audience,

            ValidateLifetime = true,

            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(conf =>
    {
        conf.RoutePrefix = string.Empty;
        conf.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotico v1");
    });
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .WithOrigins("http://localhost:4200") // Default Angular port
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();