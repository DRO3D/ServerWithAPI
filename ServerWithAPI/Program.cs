using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerWithAPI;
using ServerWithAPI.Controllers;
using ServerWithAPI.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "";

builder.Services.AddDbContext<AccountsContext>(opt =>
    opt.UseInMemoryDatabase("AccountsList"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
/*
app.MapPost("api/Values/{email}&{password}",
    async (string email, string password, ValuesController loginValue) =>
    {
        var dog = loginValue.Login(email, password);
        return dog.Result;
    });
*/
app.MapPost("api/Values/{email}/{password}", 
    async (string email, string password, ValuesController loginValue) =>
{
    var result = loginValue.Login(email, password);
    return result.Result;
});

app.MapPost("api/Values/Regist/{email}/{password}/{name}",
    async (string email, string password, string name, ValuesController loginValue) =>
    {
        var result = loginValue.Register(email, password, name);
        return result.Result;
    });

app.MapHub<ChatHub>("/chatHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
