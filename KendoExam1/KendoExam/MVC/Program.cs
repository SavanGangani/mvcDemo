
using Repositories.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IUserRepository,UserRepository>();
builder.Services.AddSingleton<IAdminRepository,AdminRepository>();
builder.Services.AddSingleton<IShoppingRepository,ShoppingRepository>();



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
