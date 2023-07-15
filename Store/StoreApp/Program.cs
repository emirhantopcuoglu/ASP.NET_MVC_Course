var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Servis kaydı eklendi

var app = builder.Build();

app.UseHttpsRedirection(); // Redirection
app.UseRouting(); // Routing 

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); 

app.Run();
