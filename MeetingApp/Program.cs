var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

//app.MapGet("/", () => "Hello World!");
// app.MapDefaultControllerRoute(); // {controller=Home}/{action=Index}/id?
// veya;

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
