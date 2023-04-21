using ProjectPrn221.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(10));

builder.Services.AddDbContext<PRN221DBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PRN221DB"));
});

var app = builder.Build();
app.UseStaticFiles();
app.UseStatusCodePagesWithRedirects("/errors/{0}");
app.UseSession();
app.MapRazorPages();

app.Run();
