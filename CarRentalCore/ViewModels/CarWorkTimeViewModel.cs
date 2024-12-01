namespace CarRental.Core.ViewModels;
public class CarWorkTimeViewModel
{
    public string Name { get; set; }
    public string Plate { get; set; }
    public decimal ActiveWorkHours { get; set; }
    public decimal MaintenanceHours { get; set; }
    public decimal IdleTime { get; set; }
}
