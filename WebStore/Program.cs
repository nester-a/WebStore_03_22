using WebStore.Conventions;
using WebStore.Infrastucture.Middleware;
using WebStore.Services;
using WebStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
builder.Services.AddSingleton<IBlogsData, InMemoryBlogsData>();
//builder.Services.AddScoped<IEmployeesData, InMemoryEmployeesData>();
//builder.Services.AddTransient<IEmployeesData, InMemoryEmployeesData>();

builder.Services.AddMvc();
builder.Services.AddControllersWithViews(opt => opt.Conventions.Add(new TestContollerConventions()))
    .AddRazorRuntimeCompilation();


var app = builder.Build();


var config = app.Configuration;

app.UseStatusCodePages();

app.UseStaticFiles();
app.UseRouting();
//app.UseMiddleware<TestMiddleware>();
app.MapGet("/greetings", () => config["Greetings"]);

app.UseStatusCodePagesWithReExecute("/Home/Status/{0}");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
