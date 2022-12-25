using Company.Common;
using Company.Data.Interface;
using Company.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CompnayContext>(
 options =>
 options.UseSqlServer(
 builder.Configuration.GetConnectionString("CompanyConnection")));

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Company.Data.Company, CompanyDto>().ReverseMap();

});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IDbService, DbService>();

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
