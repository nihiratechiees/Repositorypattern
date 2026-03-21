using Microsoft.EntityFrameworkCore;
using Repopattern.Data;
using Repopattern.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LearnDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("appconnection"));
});

builder.Services.AddScoped<IAssociateRepository, AssociateRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
