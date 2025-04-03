using MvcCoreCacheRedis.Helpers;
using MvcCoreCacheRedis.Repository;
using MvcCoreCacheRedis.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<ServiceCacheRedis>();
string cacheRedisKeys = builder.Configuration.GetValue<string>("AzureKeys:CacheRedis");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = cacheRedisKeys;
});
HelperCacheKeys.Configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddTransient<HelperPathProvider>();
builder.Services.AddTransient<RepositoryProductos>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
