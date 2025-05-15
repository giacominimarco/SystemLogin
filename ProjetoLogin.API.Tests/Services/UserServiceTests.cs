using ProjetoLogin.API.Models;
using ProjetoLogin.API.Repositories;
using ProjetoLogin.API.Services;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ProjetoLogin.API.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly IConfiguration _config;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();

            var inMemorySettings = new Dictionary<string, string?> {
                {"JwtSettings:SecretKey", "supersecretkey1234567890123456"},
                {"JwtSettings:Issuer", "issuer"},
                {"JwtSettings:Audience", "audience"},
                {"JwtSettings:ExpiresInMinutes", "30"}
            };
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _service = new UserService(_userRepoMock.Object, _config);
        }

        [Fact]
        public void Authenticate_WithValidCredentials_ReturnsUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "test", Password = "" };
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, "123");

            _userRepoMock.Setup(r => r.GetByUsername("test")).Returns(user);

            // Act
            var result = _service.Authenticate("test", "123");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test", result.Username);
        }
    }
}