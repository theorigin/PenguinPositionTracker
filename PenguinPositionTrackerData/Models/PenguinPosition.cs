namespace PenguinPositionTrackerData.Models;

public class PenguinPosition
{
    public Guid Id { get; set; }
    public Guid PenguinId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTimeOffset PositionDate { get; set; }
}