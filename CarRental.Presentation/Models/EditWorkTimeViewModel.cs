namespace CarRental.Presentation.Models;

public class EditWorkTimeViewModel
{
    public Guid Id { get; set; }
    public Guid CarId { get; set; }
    public decimal ActiveWorkHours { get; set; }
    public decimal MaintenanceHours { get; set; }
}
