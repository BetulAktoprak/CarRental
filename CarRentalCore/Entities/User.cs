using CarRental.Core.Abstractions;

namespace CarRental.Core.Entities;
public sealed class User : Entity
{
    public string FullName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = default!;
    public ICollection<Car> Cars { get; set; }
}
