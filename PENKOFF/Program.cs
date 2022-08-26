using Logic.PENKOFF;
using Logic.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Storage;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();

//
services.AddScoped<IUserManager, UserManager>();

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/Authentication/Login");
services.AddAuthorization();


/*Enable sessions*/
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add Database context
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
services.AddDbContext<BankContext>(param => param.UseSqlServer(connectionString));

var app = builder.Build();

   

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  
app.UseAuthorization();
                       
app.UseSession();      
                       
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
