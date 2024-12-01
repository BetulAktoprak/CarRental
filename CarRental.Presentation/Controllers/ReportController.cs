using CarRental.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Presentation.Controllers;
public class ReportController : Controller
{
    private readonly ICarService _carService;

    public ReportController(ICarService carService)
    {
        _carService = carService;
    }
    public async Task<IActionResult> Index(DateTime startDate, DateTime endDate)
    {
        var report = await _carService.GetAdminReportAsync(startDate, endDate);
        return View(report);
    }
}
