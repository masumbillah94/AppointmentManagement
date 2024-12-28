using Application;
using Data.SqlServer;
using Microsoft.Extensions.Configuration;
using Repository;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbServices(Configuration);
builder.Services.AddRepositoryServices();
builder.Services.AddSwaggerGenService();
builder.Services.AddCorsConfigurationService();
builder.Services.AddApplicationServices();
builder.Services.AddJWTTokenServices(Configuration);
builder.Services.AddMediatR(ctg => ctg.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjector).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
