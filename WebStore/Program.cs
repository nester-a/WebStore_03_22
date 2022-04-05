using Microsoft.EntityFrameworkCore;
using WebStore.Conventions;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Services.InMemory;
using WebStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
builder.Services.AddDbContext<WebStoreDB>(opt => 
    opt.UseSqlServer(config.GetConnectionString("SqlServer")));
builder.Services.AddTransient<WebStoreDbInitializer>();
builder.Services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
builder.Services.AddSingleton<IBlogsData, InMemoryBlogsData>();
builder.Services.AddSingleton<IProductData, InMemoryProductData>();

builder.Services.AddMvc();
builder.Services.AddControllersWithViews(opt => opt.Conventions.Add(new TestContollerConventions()))
    .AddRazorRuntimeCompilation();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<WebStoreDbInitializer>();
    await initializer.InitializeAsync();
}
var env = app.Environment;


#region Middleware

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseBrowserLink();
}
app.UseStatusCodePagesWithRedirects("~/home/status/{0}");
app.UseStaticFiles();
app.UseRouting();


# endregion


app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
