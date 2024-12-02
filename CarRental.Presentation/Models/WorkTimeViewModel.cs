namespace CarRental.Presentation.Models;

public class WorkTimeViewModel
{
    public Guid Id { get; set; }
    public decimal ActiveWorkHours { get; set; }
    public decimal MaintenanceHours { get; set; }
    public DateTime RecordedDate { get; set; }
}
