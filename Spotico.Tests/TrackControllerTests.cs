using Microsoft.AspNetCore.Mvc;
using Moq;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;
using Spotico.Infrastructure.Interfaces;
using Spotico.Server.Controllers;
using Spotico.Server.DTOs;

namespace Spotico.Tests;

public class TrackControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WhenTracksExist()
    {
        // Arrange
        var mockTrackStore = new Mock<ITrackStore>();
        mockTrackStore
            .Setup(repo => repo.GetAsync())
            .ReturnsAsync(new List<Track>
            {
                new Track
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Track",
                    Duration = "1:30",
                    ArtistId = Guid.NewGuid(),
                    AlbumId = Guid.NewGuid()
                },
                new Track
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Track",
                    Duration = "1:30",
                    ArtistId = Guid.NewGuid(),
                    AlbumId = Guid.NewGuid()
                }
            });

        var mockMediaService = new Mock<IMediaService>();
        var controller = new TrackController(mockTrackStore.Object, mockMediaService.Object);

        // Act
        var result = await controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var tracks = Assert.IsType<List<Track>>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
        Assert.NotEmpty(tracks);
    }
    
    [Fact]
    public async Task GetById_ReturnsOk_WhenTrackExists()
    {
        // Arrange
        var trackId = Guid.NewGuid();
        var mockTrackStore = new Mock<ITrackStore>();
        mockTrackStore
            .Setup(repo => repo.GetByIdAsync(trackId))
            .ReturnsAsync(new Track
            {
                Id = trackId,
                Title = "Test Track",
                Duration = "1:30",
                ArtistId = Guid.NewGuid(),
                AlbumId = Guid.NewGuid()
            });

        var mockMediaService = new Mock<IMediaService>();
        var controller = new TrackController(mockTrackStore.Object, mockMediaService.Object);

        // Act
        var result = await controller.Get(trackId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var track = Assert.IsType<Track>(okResult.Value);
        Assert.IsNotType<NotFoundResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(trackId, track.Id);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenTrackDoesntExist()
    {
        // Arrange
        var trackId = Guid.NewGuid();
        var mockTrackStore = new Mock<ITrackStore>();
        mockTrackStore
            .Setup(repo => repo.GetByIdAsync(trackId))
            .ReturnsAsync(new Track());

        var mockMediaService = new Mock<IMediaService>();
        var controller = new TrackController(mockTrackStore.Object, mockMediaService.Object);

        // Act
        var result = await controller.Get(trackId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var track = Assert.IsType<Track>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
        Assert.NotEqual(trackId, track.Id);
        Assert.Null(track.Title);
    }
    
    [Fact]
    public async Task Post_ReturnsOk_WhenTrackIsCreated()
    {
        // Arrange
        var trackDto = new TrackDTO
        {
            Title = "Test Track",
            Duration = "1:30",
            ArtistId = Guid.NewGuid(),
            File = null,
            AlbumId = Guid.NewGuid()
        };
        var mockTrackStore = new Mock<ITrackStore>();
        var mockMediaService = new Mock<IMediaService>();
        var controller = new TrackController(mockTrackStore.Object, mockMediaService.Object);

        // Act
        await controller.Post(trackDto);

        // Assert
        mockTrackStore.Verify(repo => repo.AddAsync(It.IsAny<Track>()), Times.Once);
    }
    
    [Fact]
    public async Task Put_ReturnsOk_WhenTrackIsUpdated()
    {
        // Arrange
        var track = new Track
        {
            Id = Guid.NewGuid(),
            Title = "Test Track",
            Duration = "1:30",
            ArtistId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid()
        };
        var mockTrackStore = new Mock<ITrackStore>();
        var mockMediaService = new Mock<IMediaService>();
        var controller = new TrackController(mockTrackStore.Object, mockMediaService.Object);

        // Act
        await controller.Put(track);

        // Assert
        mockTrackStore.Verify(repo => repo.UpdateAsync(It.IsAny<Track>()), Times.Once);
    }
    
    [Fact]
    public async Task Delete_ReturnsOk_WhenTrackIsDeleted()
    {
        // Arrange
        var trackId = Guid.NewGuid();
        var mockTrackStore = new Mock<ITrackStore>();
        var mockMediaService = new Mock<IMediaService>();
        var controller = new TrackController(mockTrackStore.Object, mockMediaService.Object);

        // Act
        await controller.Delete(trackId);

        // Assert
        mockTrackStore.Verify(repo => repo.DeleteAsync(trackId), Times.Once);
    }
}