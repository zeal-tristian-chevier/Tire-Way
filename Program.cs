using Microsoft.EntityFrameworkCore;
using TireWay.Models;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adds database connection - must be before app.Build();
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSession();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();