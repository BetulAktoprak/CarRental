using CarRental.Core.Entities;
using CarRental.Core.Services;
using CarRental.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CarRental.Presentation.Controllers;
public class HomeController : Controller
{
    private readonly IUserService _userService;
    private readonly IWorkTimeService _workTimeService;
    private readonly ICarService _carService;

    public HomeController(IUserService userService, IWorkTimeService workTimeService, ICarService carService)
    {
        _userService = userService;
        _workTimeService = workTimeService;
        _carService = carService;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> UserCar()
    {
        var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
        var user = await _userService.GetUserWithCarsAndWorkTimesAsync(userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Login");
        }

        var model = new UserViewModel
        {
            FullName = user.FullName,
            Cars = user.Cars?.Select(car => new CarViewModel
            {
                Id = car.Id,
                Name = car.Name,
                Plate = car.Plate,
                Price = car.Price,
                WorkTimes = car.WorkTimes?.Select(wt => new WorkTimeViewModel
                {
                    Id = wt.Id,
                    ActiveWorkHours = wt.ActiveWorkHours,
                    MaintenanceHours = wt.MaintenanceHours,
                    RecordedDate = wt.RecordedDate.Date

                }).ToList() ?? new List<WorkTimeViewModel>()
            }).ToList() ?? new List<CarViewModel>()
        };

        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> CreateUserCar()
    {
        var userName = User.Identity.Name;

        var userCars = await _carService.GetUserCarsAsync(userName);

        if (userCars == null || !userCars.Any())
        {
            TempData["Error"] = "Henüz bir araç kaydýnýz bulunmamaktadýr.";
            return RedirectToAction("UserCar");
        }

        ViewBag.Cars = userCars.Select(u => new SelectListItem
        {
            Value = u.Id.ToString(),
            Text = u.Plate
        }).ToList();

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateUserCar(Guid CarId, decimal ActiveWorkHours, decimal MaintenanceHours)
    {
        var car = await _carService.GetByIdAsync(CarId);
        if (car == null)
        {
            TempData["Error"] = "Seçilen araç bulunamadý.";
            return RedirectToAction("UserCar");
        }

        var newWorkTime = new WorkTime
        {
            CarId = CarId,
            ActiveWorkHours = ActiveWorkHours,
            MaintenanceHours = MaintenanceHours,
            RecordedDate = DateTime.Now
        };

        await _workTimeService.AddAsync(newWorkTime);

        TempData["Message"] = "Çalýþma saati ve bakým bilgisi baþarýyla eklendi.";
        return RedirectToAction("UserCar");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
