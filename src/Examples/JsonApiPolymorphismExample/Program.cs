using JsonApiDotNetCore.Configuration;
using JsonApiPolymorphismExample.Managers;
using JsonApiPolymorphismExample.Managers.Contracts;
using JsonApiPolymorphismExample.Models;
using JsonApiPolymorphismExample.Services;
using JsonApiPolymorphismExample.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddJsonApi(discovery => discovery.DefaultPageSize = new PageSize(10)
    , facade => facade.AddCurrentAssembly());

builder.Services.AddScoped<IService<ArticleBase>, ArticleService>();
builder.Services.AddScoped<IConstraintsManager, ConstraintsManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseJsonApi();

app.MapControllers();

app.Run();
