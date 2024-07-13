using Library.API.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurations
builder.Services.AddApiConfig();
builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddCorsConfig();
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