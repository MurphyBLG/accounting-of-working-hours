using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost
//     .UseKestrel()
//     .UseContentRoot(Directory.GetCurrentDirectory())
//     .UseUrls("http://*:5006")
//     .UseIISIntegration();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Allows to send http requests
builder.Services.AddHttpClient();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "front-end",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }

    );
});

// PostgresDb connecton
builder.Services.AddDbContext<AccountingOfWorkingHoursContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AOWHDb")));

// JWT auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Token Services
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IIdstoIdsPlusDataService, IdstoIdsPlusData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("front-end");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
