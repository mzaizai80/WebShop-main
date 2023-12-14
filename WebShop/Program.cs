
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Services;
using WebShopTests;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddScoped<IFileReader, FileReader>();
builder.Services.AddScoped<IFileWriter, FileWriter>();
builder.Services.Configure<ProductServiceOptions>(builder.Configuration.GetSection("ProductService"));
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<IService, Services>();
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

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//});

app.Run();

//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using WebShop.Services;
//using WebShopTests;

//var builder = WebApplication.CreateBuilder(args);

//// Add services
//builder.Services.AddSingleton<IFileReader, FileReader>();
//builder.Services.AddSingleton<IService, Services>();
//builder.Services.AddSingleton<ProductService>();
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    // Add production-specific configurations
//    // For example, app.UseExceptionHandler("/Home/Error");
//}

//// Configure routes
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
