using CarRental.Core.Entities;
using CarRental.Core.Services;
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

        ViewBag.Users = users.Select(u => new SelectListItem
        {
            Value = u.Id.ToString(),
            Text = u.FullName
        }).ToList();

        return View(new Car());
    }

    [HttpPost]
    public async Task<IActionResult> AddCar(Car car)
    {
        await _carService.AddAsync(car);

        return RedirectToAction("ListCar");
    }

    [HttpGet]
    public async Task<IActionResult> EditCar(Guid id)
    {
        var car = await _carService.GetByIdAsync(id);
        if (car == null) return NotFound();
        var users = await _userService.GetAllAsync();

        ViewBag.Users = users.Select(u => new SelectListItem
        {
            Value = u.Id.ToString(),
            Text = u.FullName
        }).ToList();
        return View("EditCar", car);
    }

    [HttpPost]
    public IActionResult EditCar(Car car)
    {

        _carService.Update(car);
        return RedirectToAction("ListCar");

    }

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
