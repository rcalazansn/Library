using Library.API.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurations
builder.Services.AddApiConfig();
builder.Services.AddFixedRate();
builder.Services.AddOutputCacheConfig();
builder.Services.AddProvidersConfig(builder.Configuration);
builder.Services.AddRabbitMqConfig(builder.Configuration);
builder.Services.AddDependencyInjectionConfig(builder.Configuration);
builder.Services.AddCorsConfig();
builder.Services.AddMediatrConfig();
builder.Services.AddHttpClientConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

//OutputCache
app.UseOutputCache();

//Configurations
app.UseApiConfig();
app.UseCorsConfig();

app.UseAuthorization();

app.MapControllers();

app.Run();