using Microsoft.Extensions.Options;
using WebShop.Models;
using WebShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.Configure<ProductServiceOptions>(builder.Configuration.GetSection("ProductService"));
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IService, Services>();

// Register other dependencies
builder.Services.AddTransient<List<Category>>();
builder.Services.AddTransient<List<Product>>();
builder.Services.AddSingleton<IOptions<ProductServiceOptions>, OptionsManager<ProductServiceOptions>>();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();