using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Demo.Middleware;
using MISA.WebFresher042023.Demo.Domain.Interface;
using MISA.WebFresher042023.Demo.Core.Service;
using MISA.WebFresher042023.Demo.Infrastructure.Repository;
using MISA.WebFresher042023.Demo.Application.Interface;
using MISA.WebFresher042023.Demo.Application.Service;
using MISA.WebFresher042023.Demo.Domain;
using MISA.WebFresher042023.Demo.Infrastructure.UnitOfWork;
using MISA.WebFresher042023.Demo.Domain.Service;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";



// Add services to the container.


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                      });
});



builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionString"];



builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IReceiptService, ReceiptService>();
builder.Services.AddScoped<IReceiptRepository,ReceiptRepository>();

builder.Services.AddScoped<IAssetManager, AssetManager>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
