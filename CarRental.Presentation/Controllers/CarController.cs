using CarRental.Core.Entities;
using CarRental.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Presentation.Controllers;
public class CarController : Controller
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }
    public async Task<IActionResult> Index()
    {
        var cars = await _carService.GetAllAsync();
        return View(cars);
    }
    [HttpGet]
    public IActionResult AddCar()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddCar(Car car)
    {
        await _carService.AddAsync(car);
        return RedirectToAction("Index");

    }
    [HttpGet]
    public async Task<IActionResult> EditCar(Guid id)
    {
        var car = await _carService.GetByIdAsync(id);
        if (car == null) return NotFound();
        return View("EditCar", car);
    }

    [HttpPost]
    public IActionResult EditCar(Car car)
    {

        _carService.Update(car);
        return RedirectToAction("Index");

    }
    public async Task<IActionResult> DeleteCar(Guid id)
    {
        var result = await _carService.DeleteByIdAsync(id);
        if (!result)
        {
            return NotFound("Silinecek kayıt bulunamadı.");
        }
        return RedirectToAction("Index");
    }
}
