var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


var app = builder.Build();


var config = app.Configuration;

app.UseStaticFiles();
app.UseRouting();
app.MapGet("/greetings", () => config["Greetings"]);
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
