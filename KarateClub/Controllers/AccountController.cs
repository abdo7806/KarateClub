using KarateClub.Core;
using KarateClub.Data.Repositories;
using KarateClub.Gloable;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly LoginViewModelRepository _LoginViewModelRepository;

    public AccountController(LoginViewModelRepository LoginViewModelRepository)
    {
        _LoginViewModelRepository = LoginViewModelRepository;
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public  IActionResult Login(LoginViewModel model)
    {
        try
        {

            var data =  _LoginViewModelRepository.Login(model);

            if (data != null)
            {
                // تعيين قيمة Session ID
                HttpContext.Session.SetString("SessionID", Guid.NewGuid().ToString());
                clsGloable._UserGloable = data;
                return RedirectToAction("Index", "Home"); // إعادة التوجيه بعد تسجيل الدخول
            }
        
            TempData["Error"] = "هاذا المستخدم غير موجود تاكد من كلمة السر او اسم المستخدم";

            return View();
        }
        catch
        {
            return View();
        }



    }

    // يمكنك إضافة طرق أخرى مثل تسجيل الخروج، إلخ.

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("SessionID");
        clsGloable._UserGloable = null;
        return RedirectToAction("Login");
    }
}