using CarRental.Business.Validations;
using CarRental.Core.Entities;
using CarRental.Core.Services;
using CarRental.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRental.Presentation.Controllers;
public class CarController : Controller
{
    private readonly ICarService _carService;
    private readonly IUserService _userService;

    public CarController(ICarService carService, IUserService userService)
    {
        _carService = carService;
        _userService = userService;
    }

    public async Task<IActionResult> ListCar()
    {
        var cars = await _carService.GetAllAsync();
        return View(cars);
    }

    [HttpGet]
    public async Task<IActionResult> AddCar()
    {
        var users = await _userService.GetAllAsync();

        return View(new Car());
    }

    [HttpPost]
    public async Task<IActionResult> AddCar(Car car)
    {
        car.Plate = car.Plate.ToUpper();
        car.Name = car.Name.ToUpper();
        var validator = new CarValidator();
        var validationResult = await validator.ValidateAsync(car);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }

            return View(car);
        }
        car.UserId = null;
        await _carService.AddAsync(car);

        return RedirectToAction("ListCar");
    }

    [HttpGet]
    public async Task<IActionResult> AssignUserToCar()
    {
        var users = await _userService.GetAllAsync();
        var cars = await _carService.GetAllAsync();

        ViewBag.Users = new SelectList(users, "Id", "FullName");
        ViewBag.Cars = new SelectList(cars, "Id", "Plate");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AssignUserToCar(Guid carId, Guid userId)
    {
        var car = await _carService.GetByIdAsync(carId);
        if (car == null)
        {
            ModelState.AddModelError(string.Empty, "Araç ve Kullanıcı seçimi zorunludur.");
            return View();
        }

        car.UserId = userId;
        await _carService.Update(car);

        return RedirectToAction("ListCar");
    }

    public async Task<IActionResult> ListAllUserCars()
    {
        var users = await _userService.GetAllAsync();

        var userCars = new List<UserViewModel>();

        foreach (var user in users)
        {
            var cars = await _carService.GetUserCarsAsync(user.Id);

            var userCarViewModel = new UserViewModel
            {
                FullName = user.FullName,
                Cars = cars.Select(car => new CarViewModel
                {
                    Plate = car.Plate,
                    Name = car.Name,
                    CreatedDate = car.CreatedDate
                }).ToList()
            };

            userCars.Add(userCarViewModel);
        }

        return View(userCars);
    }

    [HttpGet]
    public async Task<IActionResult> EditCar(Guid id)
    {
        var car = await _carService.GetByIdAsync(id);
        if (car == null) return NotFound();

        var users = await _userService.GetAllAsync();

        ViewBag.Users = new SelectList(users, "Id", "FullName");

        return View("EditCar", car);
    }

    [HttpPost]
    public async Task<IActionResult> EditCar(Car car)
    {
        car.Plate = car.Plate.ToUpper();
        car.Name = car.Name.ToUpper();

        var validator = new CarValidator();
        var validationResult = await validator.ValidateAsync(car);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }

            var users = await _userService.GetAllAsync();
            ViewBag.Users = new SelectList(users, "Id", "FullName");

            return View(car);
        }

        await _carService.Update(car);
        return RedirectToAction("ListCar");

    }
    [HttpGet]
    public async Task<IActionResult> DeleteCar(Guid id)
    {
        var result = await _carService.DeleteByIdAsync(id);
        if (!result)
        {
            return NotFound("Silinecek kayıt bulunamadı.");
        }
        return RedirectToAction("ListCar");
    }
}
