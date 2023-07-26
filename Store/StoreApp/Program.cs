using Microsoft.EntityFrameworkCore;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Servis kaydı eklendi
builder.Services.AddDbContext<RepositoryContext>(options=>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"),
    b => b.MigrationsAssembly("StoreApp"));
});

var app = builder.Build();

app.UseStaticFiles();
app.UseHttpsRedirection(); // Redirection
app.UseRouting(); // Routing 

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
