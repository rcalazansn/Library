using Library.API.Configuration;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurations
builder.Services.AddApiConfig(builder.Configuration);
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection")));
builder.Services.AddCorsConfig();
builder.Services.ResolveDependencies();
builder.Services.AddMediatr();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Configurations
app.UseApiConfig();
app.UseCorsConfig();

app.UseAuthorization();

app.MapControllers();

app.Run();