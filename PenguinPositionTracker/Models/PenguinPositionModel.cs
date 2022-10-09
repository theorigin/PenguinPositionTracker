namespace PenguinPositionTracker.Models;

public class PenguinPositionModel
{
    public Guid PenguinId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTimeOffset PositionDate { get; set; }
}