using OfficeOpenXml;
using MobileShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MobileShop.Data;
using MobileShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<PhonesShopDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PhonesShopDB")));
builder.Services.AddDbContext<MobileShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MobileShopContextConnection")));
builder.Services.AddIdentity<MobileShopUser, IdentityRole>(options =>options.SignIn.RequireConfirmedAccount = false).
AddEntityFrameworkStores<MobileShopContext>();
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = new PathString("/Identity/Account/AccessDenied");
    opt.LoginPath = new PathString("/Identity/Account/Login");
});





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
app.MapControllerRoute(
 name: "default",
 pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
