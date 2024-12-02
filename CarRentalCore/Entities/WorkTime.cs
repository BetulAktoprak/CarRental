using CarRental.Core.Abstractions;

namespace CarRental.Core.Entities;
public sealed class WorkTime : Entity
{
    public Guid CarId { get; set; }
    public Car Car { get; set; }
    public decimal ActiveWorkHours { get; set; }
    public decimal MaintenanceHours { get; set; }
    public decimal IdleTime { get; set; }
    public DateTime RecordedDate { get; set; } = DateTime.UtcNow;
}
