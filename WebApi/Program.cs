using BusinessLogic.Profiles;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services;
using DomainData.UoW;
using MenuManager.DB;
using Lab7.Controllers;
using DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MenuContext>();

builder.Services.AddAutoMapper(typeof(DailyMenuProfile).Assembly);
builder.Services.AddAutoMapper(typeof(DishDTO).Assembly);

builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDailyMenuService, DailyMenuService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<DishController, DishController>();
builder.Services.AddScoped<DailyMenuController, DailyMenuController>();
builder.Services.AddScoped<OrdersController, OrdersController>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});
var app = builder.Build();
app.UseCors("AllowLocalhost3000");

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
