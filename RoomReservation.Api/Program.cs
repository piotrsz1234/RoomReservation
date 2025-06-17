using Microsoft.EntityFrameworkCore;
using RoomReservation.Api;
using RoomReservation.Implementation;
using RoomReservation.Implementation.Aspects;
using RoomReservation.Implementation.DbContexts;
using RoomReservation.Implementation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRoomReservationImplementation(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessionHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var context = app.Services.CreateScope().ServiceProvider.GetService<MainDbContext>();
context.Database.Migrate();
SeedingService.Seed(context);
LogQueryTimeAttribute.Logger = app.Services.GetService<ILogger<LogQueryTimeAttribute>>();

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();