using Microsoft.AspNetCore.Authentication.Cookies;
using RoomReservation.Application.Helpers;
using RoomReservation.Application.Services;
using RoomReservation.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddApiImplementation();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(10);
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<SessionHelper>();
builder.Services.AddScoped<DropdownHelper>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/User/SignIn";
    options.LogoutPath = "/User/Logout";
});

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