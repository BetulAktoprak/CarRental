using CarRental.Business.Validations;
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
        var validator = new UserValidator();
        var validationResult = validator.Validate(user);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }
            return View(user);
        }

        await _userService.RegisterAsync(user);
        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var validator = new LoginValidator();
        var validationResult = validator.Validate((email, password));

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }
            return View();
        }

        var user = await _userService.AuthenticateAsync(email, password);
        if (user == null)
        {
            ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre!";
            return View();
        }

        HttpContext.Session.SetString("UserId", user.Id.ToString());
        HttpContext.Session.SetString("Role", user.Role);

        if (user.Role == Roles.Admin)
        {
            return RedirectToAction("ListCar", "Car");
        }
        else
        {
            return RedirectToAction("ListUserCar", "Home");
        }
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Login");
    }
}
