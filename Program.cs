using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STMIS.Data;
using STMIS.Helpers;
using STMIS.Interfaces;
using STMIS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(e =>
    e.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 5;
     opt.Password.RequireLowercase = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
    opt.Lockout.MaxFailedAccessAttempts = 5;
//opt.SignIn.RequireConfirmedAccount = true;


});
builder.Services.AddAuthentication()
.AddFacebook(options =>
{
    options.AppId = "628700462369980";
    options.AppSecret = "748f89b45a40708be8ff66bea5475114";
})
.AddGoogle(options =>
{
    options.ClientId = "790020427308-ebl0hdllq7t625bgia3dtq506e8om9g1.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("SendGrid"));
builder.Services.AddTransient<ISendGridEmail, SendGridEmail>();
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

app.Run();
