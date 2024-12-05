namespace CarRental.Presentation.Models;

public class CarViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Plate { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public List<WorkTimeViewModel> WorkTimes { get; set; }
}
