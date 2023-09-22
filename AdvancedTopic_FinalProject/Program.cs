using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AdvancedTopicsAuthDemo.Data;
using AdvancedTopicsAuthDemo.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ATAuthDemoContextConnection") ?? throw new InvalidOperationException("Connection string 'ATAuthDemoContextConnection' not found.");

builder.Services.AddDbContext<ATAuthDemoContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<DemoUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ATAuthDemoContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
