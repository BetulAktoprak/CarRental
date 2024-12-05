using CarRental.Core.Abstractions;

namespace CarRental.Core.Entities;
public sealed class Car : Entity
{
    public string Name { get; set; } = default!;
    public string Plate { get; set; } = default!;
    public decimal Price { get; set; }
    public ICollection<WorkTime> WorkTimes { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
}
