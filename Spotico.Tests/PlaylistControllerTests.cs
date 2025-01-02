using Microsoft.AspNetCore.Mvc;
using Moq;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;
using Spotico.Server.Controllers;
using Spotico.Server.DTOs;

namespace Spotico.Tests;

public class PlaylistControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WhenPlaylistsExist()
    {
        // Arrange
        var mockPlaylistStore = new Mock<IPlaylistStore>();
        mockPlaylistStore
            .Setup(repo => repo.GetAsync())
            .ReturnsAsync(new List<Playlist>
            {
                new Playlist
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Playlist",
                    Description = "Test Description",
                    TrackIds = new List<Guid>(),
                    CreatedBy = new User { Id = Guid.NewGuid() },
                    IsPublic = true
                },
                new Playlist
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Playlist",
                    Description = "Test Description",
                    TrackIds = new List<Guid>(),
                    CreatedBy = new User { Id = Guid.NewGuid() },
                    IsPublic = true
                }
            });

        var controller = new PlaylistController(mockPlaylistStore.Object);

        // Act
        var result = await controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var playlists = Assert.IsType<List<Playlist>>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
        Assert.NotEmpty(playlists);
    }
    
    [Fact]
    public async Task GetById_ReturnsOk_WhenPlaylistExist()
    {
        // Arrange
        var playlistId = Guid.NewGuid();
        var mockPlaylistStore = new Mock<IPlaylistStore>();
        mockPlaylistStore
            .Setup(repo => repo.GetByIdAsync(playlistId))
            .ReturnsAsync(new Playlist
            {
                Id = playlistId,
                Title = "Test Playlist",
                Description = "Test Description",
                TrackIds = new List<Guid>(),
                CreatedBy = new User { Id = Guid.NewGuid() },
                IsPublic = true
            });

        var controller = new PlaylistController(mockPlaylistStore.Object);

        // Act
        var result = await controller.Get(playlistId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var playlist = Assert.IsType<Playlist>(okResult.Value);
        Assert.IsNotType<NotFoundResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(playlistId, playlist.Id);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenPlaylistDoesntExist()
    {
        // Arrange
        var playlistId = Guid.NewGuid();
        var mockPlaylistStore = new Mock<IPlaylistStore>();
        mockPlaylistStore
            .Setup(repo => repo.GetByIdAsync(playlistId))
            .ReturnsAsync(new Playlist());

        var controller = new PlaylistController(mockPlaylistStore.Object);

        // Act
        var result = await controller.Get(playlistId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var playlist = Assert.IsType<Playlist>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
        Assert.NotEqual(playlistId, playlist.Id);
        Assert.Null(playlist.Title);
        Assert.Null(playlist.Description);
    }
    
    [Fact]
    public async Task Post_ReturnsOk_WhenPlaylistIsCreated()
    {
        // Arrange
        var playlistDto = new PlaylistDTO
        {
            Title = "Test Playlist",
            Description = "Test Description",
            CreatedById = Guid.NewGuid(),
            IsPublic = true
        };
        var mockPlaylistStore = new Mock<IPlaylistStore>();
        var controller = new PlaylistController(mockPlaylistStore.Object);

        // Act
        await controller.Post(playlistDto);

        // Assert
        mockPlaylistStore.Verify(repo => repo.AddAsync(It.IsAny<Playlist>()), Times.Once);
    }
    
    [Fact]
    public async Task Put_ReturnsOk_WhenPlaylistIsUpdated()
    {
        // Arrange
        var playlist = new Playlist
        {
            Id = Guid.NewGuid(),
            Title = "Test Playlist",
            Description = "Test Description",
            CreatedBy = new User { Id = Guid.NewGuid() },
            IsPublic = true
        };
        var mockPlaylistStore = new Mock<IPlaylistStore>();
        var controller = new PlaylistController(mockPlaylistStore.Object);

        // Act
        await controller.Put(playlist);

        // Assert
        mockPlaylistStore.Verify(repo => repo.UpdateAsync(It.IsAny<Playlist>()), Times.Once);
    }
    
    [Fact]
    public async Task Delete_ReturnsOk_WhenPlaylistIsDeleted()
    {
        // Arrange
        var playlistId = Guid.NewGuid();
        var mockPlaylistStore = new Mock<IPlaylistStore>();
        var controller = new PlaylistController(mockPlaylistStore.Object);

        // Act
        await controller.Delete(playlistId);

        // Assert
        mockPlaylistStore.Verify(repo => repo.DeleteAsync(playlistId), Times.Once);
    }
}