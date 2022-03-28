using WebStore.Conventions;
using WebStore.Services;
using WebStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
builder.Services.AddSingleton<IBlogsData, InMemoryBlogsData>();
builder.Services.AddSingleton<IProductData, InMemoryProductData>();

builder.Services.AddMvc();
builder.Services.AddControllersWithViews(opt => opt.Conventions.Add(new TestContollerConventions()))
    .AddRazorRuntimeCompilation();


var app = builder.Build();
var env = app.Environment;

var config = app.Configuration;

#region Middleware

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseBrowserLink();
}
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseRouting();
app.UseStatusCodePagesWithReExecute("/Home/Status/{0}");


# endregion

app.MapGet("/greetings", () => config["Greetings"]);

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
