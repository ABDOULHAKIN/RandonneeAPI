using Microsoft.EntityFrameworkCore;
using NewZWalkAPI.Data;
using NewZWalkAPI.Mappings;
using NewZWalkAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  On veut injecter notre DBContext et pour cela 
// On va utiliser le builder.services
builder.Services.AddDbContext<NZWalkDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalkConnectionString")));

// On injecter le Repository venant de SQLRegionRepository
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

// Injecte l'automapper dans l'app
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
