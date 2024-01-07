using Microsoft.Extensions.Options;
using WebShop.Services;
var builder = WebApplication.CreateBuilder(args);
// Register services
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.Configure<ProductServiceOptions>(builder.Configuration.GetSection("ProductService"));
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<IService, Services>();
// Register other dependencies
builder.Services.AddScoped<IOptions<ProductServiceOptions>, OptionsManager<ProductServiceOptions>>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();