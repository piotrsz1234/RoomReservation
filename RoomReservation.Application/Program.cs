using RoomReservation.Application.Helpers;
using RoomReservation.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRoomReservationImplementation(builder.Configuration);
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessionHelper>();
builder.Services.AddAuthentication().AddCookie();
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();