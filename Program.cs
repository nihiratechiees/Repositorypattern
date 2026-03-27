using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repopattern.Data;
using Repopattern.Repository;
using Repopattern.Repository.Implementation;
using Repopattern.Repository.Interface;
using Repopattern.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LearnDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("appconnection"));
});

builder.Services.AddScoped<IAssociateRepository, AssociateRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JWTService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    var key = builder.Configuration["Jwtsettings:key"];
    var _key=Encoding.UTF8.GetBytes(key);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwtsettings:Issuer"],
        ValidAudience = builder.Configuration["Jwtsettings:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(_key)
    };
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("addcors", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4201")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("addcors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
