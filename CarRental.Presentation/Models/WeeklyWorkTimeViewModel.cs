namespace CarRental.Presentation.Models;

public class WeeklyWorkTimeViewModel
{
    public string Name { get; set; }
    public string Plate { get; set; }
    public decimal ActiveWorkHours { get; set; }
    public decimal MaintenanceHours { get; set; }
    public decimal IdleTime { get; set; }
    public decimal ActiveWorkPercentage { get; set; }
    public decimal IdleTimePercentage { get; set; }
}
