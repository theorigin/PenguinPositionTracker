using Microsoft.AspNetCore.Mvc;
using PenguinPositionTracker.Models;
using PenguinPositionTrackerData;

namespace PenguinPositionTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class PenguinPositionController : ControllerBase
{
    private readonly IPenguinData _penguinData;

    public PenguinPositionController(IPenguinData penguinData)
    {
        _penguinData = penguinData;
    }

    [HttpPost(Name = "PostPenguinPosition")]
    public IActionResult Post(PenguinPositionModel position)
    {
        var positionId = _penguinData.AddPosition(position.PenguinId, position.PositionDate, position.Latitude, position.Longitude);
        return CreatedAtAction(nameof(Get), new { positionId }, position);
    }

    [HttpGet(Name = "GetPenguinPosition")]
    public IActionResult Get(Guid positionId)
    {
        var position = _penguinData.GetPosition(positionId);

        return position != null ? Ok(position) : NotFound();
    }
}