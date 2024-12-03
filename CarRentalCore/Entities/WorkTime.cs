using CarRental.Core.Abstractions;

namespace CarRental.Core.Entities;
public sealed class WorkTime : Entity
{
    public Guid CarId { get; set; }
    public Car Car { get; set; }
    public decimal ActiveWorkHours { get; set; } = 0;
    public decimal MaintenanceHours { get; set; } = 0;
    public decimal IdleTime { get; set; } = 0;
    public DateTime RecordedDate { get; set; } = DateTime.UtcNow;
}
