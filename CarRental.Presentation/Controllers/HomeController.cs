using CarRental.Business.Validations;
using CarRental.Core.Entities;
using CarRental.Core.Services;
using CarRental.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> ListUserCar()
    {
        var userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
        var user = await _userService.GetUserCarsAsync(userId);

        if (user is null)
        {
            return RedirectToAction("Login", "Login");
        }

        var model = new UserViewModel
        {
            FullName = user.FullName,
            Cars = user.Cars.Select(car => new CarViewModel
            {
                Name = car.Name,
                Plate = car.Plate,
                Price = car.Price
            }).ToList() ?? new List<CarViewModel>()
        };

        return View(model);
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
        var userIdString = HttpContext.Session.GetString("UserId");

        if (string.IsNullOrEmpty(userIdString))
        {
            return RedirectToAction("Login", "Login");
        }

        var userId = Guid.Parse(userIdString);

        var userCars = await _carService.GetUserCarsAsync(userId);

        if (userCars == null || !userCars.Any())
        {
            TempData["Error"] = "Henüz bir araç kaydýnýz bulunmamaktadýr.";
            return RedirectToAction("UserCar");
        }

        ViewBag.Cars = userCars;

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateUserCar(WorkTime workTime)
    {
        var validator = new WorkTimeValidator();
        var validationResult = await validator.ValidateAsync(workTime);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }

            return View(workTime);
        }
        await _workTimeService.AddAsync(workTime);

        var userIdString = HttpContext.Session.GetString("UserId");

        if (!string.IsNullOrEmpty(userIdString))
        {
            var userId = Guid.Parse(userIdString);
            ViewBag.Cars = _carService.GetUserCarsAsync(userId);
        }

        TempData["Message"] = "Çalýþma saati ve bakým bilgisi baþarýyla eklendi.";
        return RedirectToAction("UserCar");
    }

    [HttpGet]
    public async Task<IActionResult> EditUserCar(Guid id)
    {
        var workTime = await _workTimeService.GetByIdAsync(id);
        if (workTime is null)
        {
            return NotFound();
        }

        var model = new EditWorkTimeViewModel
        {
            Id = workTime.Id,
            CarId = workTime.CarId,
            ActiveWorkHours = workTime.ActiveWorkHours,
            MaintenanceHours = workTime.MaintenanceHours
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUserCar(EditWorkTimeViewModel model)
    {

        var workTime = await _workTimeService.GetByIdAsync(model.Id);
        if (workTime is null)
        {
            return NotFound();
        }
        var validator = new WorkTimeValidator();
        var validationResult = await validator.ValidateAsync(workTime);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }

            return View(workTime);
        }
        workTime.ActiveWorkHours = model.ActiveWorkHours;
        workTime.MaintenanceHours = model.MaintenanceHours;
        workTime.RecordedDate = DateTime.UtcNow;

        await _workTimeService.Update(workTime);

        return RedirectToAction("UserCar", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
