using Microsoft.AspNetCore.Mvc;
using Moq;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;
using Spotico.Server.Controllers;
using Spotico.Server.DTOs;

namespace Spotico.Tests;

public class AlbumControllerTests
{
    private readonly Mock<IAlbumStore> _mockAlbumStore;
    private readonly AlbumController _controller;

    public AlbumControllerTests()
    {
        _mockAlbumStore = new Mock<IAlbumStore>();
        _controller = new AlbumController(_mockAlbumStore.Object);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WithListOfAlbums()
    {
        // Arrange
        var albums = new List<Album> { new Album { Id = Guid.NewGuid(), Title = "Test Album" } };
        _mockAlbumStore.Setup(store => store.GetAsync()).ReturnsAsync(albums);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Album>>(okResult.Value);
        Assert.Equal(albums.Count, returnValue.Count);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithAlbum()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        var album = new Album { Id = albumId, Title = "Test Album" };
        _mockAlbumStore.Setup(store => store.GetByIdAsync(albumId)).ReturnsAsync(album);

        // Act
        var result = await _controller.Get(albumId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Album>(okResult.Value);
        Assert.Equal(albumId, returnValue.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenAlbumDoesNotExist()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        _mockAlbumStore.Setup(store => store.GetByIdAsync(albumId)).ReturnsAsync((Album)null);

        // Act
        var result = await _controller.Get(albumId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Post_ReturnsOkResult()
    {
        // Arrange
        var album = new AlbumDTO(
            Title: "Test Album", 
            ArtistId: Guid.NewGuid(),
            CoverImage: null
        );

        // Act
        var result = await _controller.Post(album);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsOkResult()
    {
        // Arrange
        var album = new Album { Id = Guid.NewGuid(), Title = "Updated Album" };

        // Act
        var result = await _controller.Put(album);

        // Assert
        _mockAlbumStore.Verify(store => store.UpdateAsync(album), Times.Once);
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsOkResult()
    {
        // Arrange
        var albumId = Guid.NewGuid();

        // Act
        var result = await _controller.Delete(albumId);

        // Assert
        _mockAlbumStore.Verify(store => store.DeleteAsync(albumId), Times.Once);
        Assert.IsType<OkResult>(result);
    }
}