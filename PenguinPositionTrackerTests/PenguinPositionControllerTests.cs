using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PenguinPositionTracker.Controllers;
using PenguinPositionTracker.Models;
using PenguinPositionTrackerData;

namespace PenguinPositionTrackerTests;

public class PenguinPositionControllerTests
{
    private readonly Mock<IPenguinData> _penguinDataMock;

    public PenguinPositionControllerTests()
    {
        _penguinDataMock = new Mock<IPenguinData>();
    }

    [Fact]
    public void Post_Calls_RecordPosition()
    {
        var position = new PenguinPositionModel();
        var controller = new PenguinPositionController(_penguinDataMock.Object);

        controller.Post(position);

        _penguinDataMock.Verify(x => x.AddPosition(position.PenguinId, position.PositionDate, position.Latitude, position.Longitude));
    }

    [Fact]
    public void Post_Returns_Created_Response()
    {
        var position = new PenguinPositionModel();
        var expectedReturnPositionId = Guid.NewGuid();
        _penguinDataMock
            .Setup(x => x.AddPosition(It.IsAny<Guid>(), It.IsAny<DateTimeOffset>(), It.IsAny<double>(),
                It.IsAny<double>())).Returns(expectedReturnPositionId);
        var controller = new PenguinPositionController(_penguinDataMock.Object);
        var response = controller.Post(position);

        response.Should().BeOfType<CreatedAtActionResult>();
    }
}