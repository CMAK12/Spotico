using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Spotico.Core.Database;
using Spotico.Infrastructure;
using Moq;
using Spotico.Core.Models;

namespace Spotico.Tests;

public class TokenServiceTests
{
    [Fact]
    public void IsTokenGenerated()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<SpoticoDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
        var dbContext = new SpoticoDbContext(options);
        
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Username = "TestUser",
            Password = "TestPassword",
            Email = "example@gmail.com"
        };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        
        var authOptions = new AuthOptions
        {
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            TokenLifetime = 3600,
            Secret = "TestSecretKey12345678901234567890"
        };
        
        var mockOptions = new Mock<IOptions<AuthOptions>>();
        mockOptions.Setup(o => o.Value).Returns(authOptions);
        
        // Act
        var tokenService = new TokenService(dbContext, mockOptions.Object);
        var token = tokenService.GenerateToken(user);
        
        // Assert
        Assert.NotNull(token);
    }

    [Fact]
    public void IsTokenContainsCorrectUserInfo()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<SpoticoDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
        using var dbContext = new SpoticoDbContext(options);
        
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Username = "TestUser",
            Password = "TestPassword",
            Email = "example@gmail.com"
        };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        
        var authOptions = new AuthOptions
        {
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            TokenLifetime = 3600,
            Secret = "TestSecretKey12345678901234567890"
        };
        
        var mockOptions = new Mock<IOptions<AuthOptions>>();
        mockOptions.Setup(o => o.Value).Returns(authOptions);

        // Act
        var tokenService = new TokenService(dbContext, mockOptions.Object);
        var token = tokenService.GenerateToken(user);
        
        // Assert
        var handler = new JwtSecurityTokenHandler();
        var tokenData = handler.ReadJwtToken(token);
        var usernameClaim = tokenData.Claims.First(c => 
            c.Type == JwtRegisteredClaimNames.Name).Value;
        var userIdClaim = tokenData.Claims.First(c =>
            c.Type == JwtRegisteredClaimNames.Sub).Value;
        var userRoleClaim = tokenData.Claims.First(c =>
            c.Type == ClaimTypes.Role).Value;
        
        Assert.Equal(user.Username, usernameClaim);
        Assert.Equal(user.Id.ToString(), userIdClaim);
        Assert.Equal(user.Role, userRoleClaim);
    }
}