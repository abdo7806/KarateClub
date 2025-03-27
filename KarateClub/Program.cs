using KarateClub.Core;
using KarateClub.Core.ViewModel;
using KarateClub.Data;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ����� ����� �������
builder.Services.AddDbContext<ApplicationDbContext2>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ����� ������� ��� �������.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews(); // ����� MVC � Razor Pages

// ����� ����������
builder.Services.AddScoped<IRepository<Person>, PersonRepository>();
builder.Services.AddScoped<IRepository<Member>, MemberRepository>();
builder.Services.AddScoped<IRepository<BeltRank>, BeltRankRepository>();
builder.Services.AddScoped<IRepository<Instructor>, InstructorRepository>();
builder.Services.AddScoped<IRepository<Payment>, PaymentRepository>();
builder.Services.AddScoped<IRepository<SubscriptionPeriod>, SubscriptionPeriodRepository>();
builder.Services.AddScoped<IRepository<BeltTest>, BeltTestRepository>();
builder.Services.AddScoped<IRepository<MemberInstructor>, MemberInstructorRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<LoginViewModelRepository>();

// ����� ��� �������
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // ��� ������ ������
    options.Cookie.HttpOnly = true; // ���� ������� ��� ����� ������ ��� JavaScript
    options.Cookie.IsEssential = true; // ���� �� �� ������� ������� ��� �� ���� ����� �������� �����
});



var app = builder.Build();

// ������� ����...

// ����� �� ������ ����� HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // ������ HSTS �� ����� �������
}
app.UseHttpsRedirection(); // ����� ������� ��� HTTPS
app.UseStaticFiles(); // ���� ������� �������

app.UseRouting(); // ����� �������

// ������� �������
app.UseSession();

// ����� Middleware ������ �� ������
app.UseMiddleware<SessionMiddleware>();



//app.UseAuthentication(); // ����� Authentication
app.UseAuthorization(); // ����� Authorization


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // ����� �������� ���������� �� MVC

app.Run(); // ��� ����� �������