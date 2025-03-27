using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class SessionMiddleware
{
    private readonly RequestDelegate _next;

    public SessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // تحقق مما إذا كان المستخدم في صفحة تسجيل الدخول
        var path = context.Request.Path.Value.ToLower();
        if (path.StartsWith("/account/login"))
        {
            // إذا كان في صفحة تسجيل الدخول، تابع الطلب
            await _next(context);
            return;
        }

        var sessionId = context.Session.GetString("SessionID");
        Console.WriteLine($"Current SessionId: {sessionId}");
        if (sessionId == null)
        {
            Console.WriteLine("SessionId not found. Redirecting to Login.");
            context.Response.Redirect("/Account/Login");
            return;
        }

        // تابع إلى الطلب التالي إذا كان الجلسة موجودة
        await _next(context);
    }
}