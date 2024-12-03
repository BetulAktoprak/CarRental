using CarRental.Core.Services;
using CarRental.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Presentation.Controllers;
public class ReportController : Controller
{
    private readonly ICarService _carService;
    private readonly IWorkTimeService _workTimeService;

    public ReportController(ICarService carService, IWorkTimeService workTimeService)
    {
        _carService = carService;
        _workTimeService = workTimeService;
    }

    public async Task<List<WeeklyWorkTimeViewModel>> GetWeeklyWorkTimeAsync()
    {
        var lastWeek = DateTime.Now.AddDays(-7);
        var workTimes = await _workTimeService.GetAllWithFilterAsync(wt => wt.RecordedDate >= lastWeek);

        var groupedData = workTimes
            .Where(wt => wt.Car != null)
            .GroupBy(wt => wt.CarId)
            .Select(g =>
            {
                var firstWorkTime = g.FirstOrDefault();
                return new WeeklyWorkTimeViewModel
                {
                    Name = firstWorkTime?.Car?.Name ?? "Bilinmiyor",
                    Plate = firstWorkTime?.Car?.Plate ?? "Bilinmiyor",
                    ActiveWorkHours = g.Sum(wt => wt.ActiveWorkHours),
                    MaintenanceHours = g.Sum(wt => wt.MaintenanceHours),
                    IdleTime = (7 * 24) - g.Sum(wt => wt.ActiveWorkHours + wt.MaintenanceHours)
                };
            })
            .ToList();
        return groupedData;
    }

    public async Task<IActionResult> Index()
    {
        var weeklyWorkTimes = await GetWeeklyWorkTimeAsync();

        return View(weeklyWorkTimes);
    }
    public async Task<IActionResult> ActiveWorkChart()
    {
        var lastWeek = DateTime.Now.AddDays(-7);

        // Servis üzerinden veriyi alıyoruz
        var workTimes = await _workTimeService.GetAllWithFilterAsync(wt => wt.RecordedDate >= lastWeek);

        // Araç verilerini grup haline getiriyoruz
        var groupedData = workTimes
            .Where(wt => wt.Car != null)
            .GroupBy(wt => wt.CarId)
            .Select(g =>
            {
                var totalHours = g.Sum(wt => wt.ActiveWorkHours);
                var totalWeeklyHours = 7 * 24; // Bir haftadaki toplam saat
                var percentage = (totalHours / totalWeeklyHours) * 100; // Yüzdelik değer

                return new
                {
                    Name = g.FirstOrDefault()?.Car?.Name ?? "Bilinmiyor",
                    ActiveWorkPercentage = percentage
                };
            })
            .ToList();

        // ViewBag ile veriyi View'e taşıyoruz
        ViewBag.ChartData = groupedData;

        // View'e dönüyoruz
        return View();
    }
}
