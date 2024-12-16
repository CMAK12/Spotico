using Microsoft.AspNetCore.Mvc;
using Moq;
using Spotico.Core.Models;
using Spotico.Core.Stores;
using Spotico.Server.Controllers;
using Spotico.Server.DTOs;

namespace Spotico.Tests;

public class CustomerControllerTests
{
    [Fact]
    public async Task GetCustomer_ReturnsOk_WhenCustomerExists()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var mockUserStore = new Mock<IUserStore>();
        mockUserStore
            .Setup(repo => repo.GetByIdAsync(customerId))
            .ReturnsAsync(new User
            {
                Id = customerId, 
                Username = "TestUser", 
                Email = "example@test.com", 
                Password = "secret"
            });

        var controller = new CustomerController(mockUserStore.Object);

        // Act
        var result = await controller.Get(customerId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var user = Assert.IsType<User>(okResult.Value);
        Assert.Equal(okResult.StatusCode, 200);
        Assert.Equal(customerId, user.Id);
    }

    [Fact]
    public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var mockUserStore = new Mock<IUserStore>();
        mockUserStore
            .Setup(repo => repo.GetByIdAsync(customerId))
            .ReturnsAsync((User)null); // Simulate no user found

        var controller = new CustomerController(mockUserStore.Object);

        // Act
        var result = await controller.Get(customerId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}