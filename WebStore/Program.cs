using WebStore.Conventions;
using WebStore.Infrastucture.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc();
builder.Services.AddControllersWithViews(opt => opt.Conventions.Add(new TestContollerConventions))
    .AddRazorRuntimeCompilation();


var app = builder.Build();


var config = app.Configuration;

app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<TestMiddleware>();
app.MapGet("/greetings", () => config["Greetings"]);
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
