using KarateClub.Core;
using KarateClub.Core.ViewModel;
using KarateClub.Data;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ÅÚÏÇÏ ÓáÓáÉ ÇáÇÊÕÇá
builder.Services.AddDbContext<ApplicationDbContext2>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ÅÖÇİÉ ÇáÎÏãÇÊ Åáì ÇáÍÇæíÉ.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews(); // ÊİÚíá MVC æ Razor Pages

// ÊÓÌíá ÇáãÓÊæÏÚÇÊ
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

// ÅÖÇİÉ ÏÚã ÇáÌáÓÇÊ
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // æŞÊ ÇäÊåÇÁ ÇáÌáÓÉ
    options.Cookie.HttpOnly = true; // ÊÌÚá ÇáßæßíÒ ÛíÑ ŞÇÈáÉ ááæÕæá ÚÈÑ JavaScript
    options.Cookie.IsEssential = true; // ÊÃßÏ ãä Ãä ÇáßæßíÒ ÊõÓÊÎÏã ÍÊì áæ ßÇäÊ ÓíÇÓÉ ÇáÎÕæÕíÉ ÕÇÑãÉ
});



var app = builder.Build();

// ÅÚÏÇÏÇÊ ÃÎÑì...

// Êßæíä ÎØ ÃäÇÈíÈ ÇáØáÈ HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // ÇÓÊÎÏã HSTS İí ÈíÆÇÊ ÇáÅäÊÇÌ
}
app.UseHttpsRedirection(); // ÅÚÇÏÉ ÇáÊæÌíå Åáì HTTPS
app.UseStaticFiles(); // ÎÏãÉ ÇáãáİÇÊ ÇáËÇÈÊÉ

app.UseRouting(); // ÊåíÆÉ ÇáÊæÌíå

// ÇÓÊÎÏÇã ÇáÌáÓÇÊ
app.UseSession();

// ÅÖÇİÉ Middleware ááÊÍŞŞ ãä ÇáÌáÓÉ
app.UseMiddleware<SessionMiddleware>();



//app.UseAuthentication(); // ÊİÚíá Authentication
app.UseAuthorization(); // ÊİÚíá Authorization


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // ÊÚííä ÇáãÓÇÑÇÊ ÇáÇİÊÑÇÖíÉ áÜ MVC

app.Run(); // ÈÏÁ ÊÔÛíá ÇáÊØÈíŞ