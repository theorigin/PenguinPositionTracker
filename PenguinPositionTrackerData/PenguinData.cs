using LiteDB;
using PenguinPositionTrackerData.Models;

namespace PenguinPositionTrackerData;

public interface IPenguinData
{
    Guid AddPosition(Guid penguinId, DateTimeOffset positionDate, double latitude, double longitude);
    PenguinPosition? GetPosition(Guid positionId);
}

public class PenguinData : IPenguinData
{
    private readonly ILiteDatabase _liteDatabase;
    
    public PenguinData(ILiteDatabase liteDatabase)
    {
        _liteDatabase = liteDatabase;
    }

    public Guid AddPosition(Guid penguinId, DateTimeOffset positionDate, double latitude, double longitude)
    {
        var doc = new PenguinPosition
        {
            Id = Guid.NewGuid(),
            PenguinId = penguinId,
            PositionDate = positionDate,
            Latitude = latitude, 
            Longitude = longitude
        };

        GetPenguinPositionCollection().Insert(doc);

        return doc.Id;
    }

    public PenguinPosition? GetPosition(Guid positionId) =>
        GetPenguinPositionCollection().FindById(positionId);

    private ILiteCollection<PenguinPosition> GetPenguinPositionCollection() =>
        _liteDatabase.GetCollection<PenguinPosition>("PenguinPosition");
}