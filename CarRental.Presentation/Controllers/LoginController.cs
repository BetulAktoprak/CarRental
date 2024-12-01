using CarRental.Core.Entities;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Presentation.Controllers;
public class LoginController : Controller
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User user)
    {
        await _userService.RegisterAsync(user);
        return View("Index", "Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string email, string password)
    {
        var user = await _userService.AuthenticateAsync(username, email, password);
        if (user == null)
        {
            ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre!";
            return View();
        }

        HttpContext.Session.SetString("UserId", user.Id.ToString());
        HttpContext.Session.SetString("Role", user.Role);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Login");
    }
}
