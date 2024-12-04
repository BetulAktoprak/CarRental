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
                var totalWorkHours = g.Sum(wt => wt.ActiveWorkHours + wt.MaintenanceHours);
                var activeWorkHours = g.Sum(wt => wt.ActiveWorkHours);

                return new WeeklyWorkTimeViewModel
                {
                    Name = firstWorkTime?.Car?.Name ?? "Bilinmiyor",
                    Plate = firstWorkTime?.Car?.Plate ?? "Bilinmiyor",
                    ActiveWorkHours = activeWorkHours,
                    MaintenanceHours = g.Sum(wt => wt.MaintenanceHours),
                    IdleTime = 168 - totalWorkHours,
                    IdleTimePercentage = ((168 - totalWorkHours) / 168) * 100,
                    ActiveWorkPercentage = (activeWorkHours / 168) * 100
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

    [HttpGet]
    public async Task<IActionResult> ActiveWorkChart()
    {
        var data = await GetWeeklyWorkTimeAsync();
        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> IdleTimeChart()
    {
        var data = await GetWeeklyWorkTimeAsync();
        return View(data);
    }
}
