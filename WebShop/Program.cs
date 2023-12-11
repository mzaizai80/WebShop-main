using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Services;
using WebShopTests;
// using WebShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddSingleton<IService, Services>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Add production-specific configurations
    // For example, app.UseExceptionHandler("/Home/Error");
}

// Configure routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<ProductService>();
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
////app.MapGet("/", () => "Hello World!");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{id}");


//app.Run();
