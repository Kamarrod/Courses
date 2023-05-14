using Microsoft.EntityFrameworkCore;
using Courses.DAL;
using Courses.DAL.Interfaces;
using Courses.DAL.Repositories;
using Courses.Service.Interfaces;
using Courses.Service.Implementations;
using Ñourses.Domain.Entity;
using Courses.Domain.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(@"Server=LAPTOP-F15VG6AA\SQLEXPRESS;Database=BaseCourse;Trusted_Connection=True;TrustServerCertificate=False;Encrypt=False;"));
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IBaseRepository<PracticalPart>, PracticalPartRepository>();
builder.Services.AddScoped<IPracticalPartService, PracticalPartService>();
//builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
builder.Services.AddScoped<IBaseRepository<CompletedPart>, CompletedPartsRepository>();
builder.Services.AddScoped<ICompletedPartService, CompletedPartService>();
builder.Services.AddScoped<IBaseRepository<CompletedCourse>, CompletedCourseRepository>();
builder.Services.AddScoped<ICompletedCourseService, CompletedCourseService>();
//builder.Services.AddScoped<SignInManager<IdentityUser>>();
//builder.Services.AddScoped < UserManager<IdentityUser>();

////builder.Services.AddScoped<IBaseRepository<Profile>, ProfileRepository>();
////builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddIdentity< User, IdentityRole >()
    .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

builder.Services.AddAuthentication();
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
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
