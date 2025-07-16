using backend.Controllers;
using backend.DTOs.Login;
using backend.Services.Login;
using backend.Services.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ArgusTrialTest
{
    public class LoginControllerTests
    {
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<ITokenService> _mockTokenRepo;
        private Mock<ILoginService> _mockLoginService;
        private LoginController _controller;

        [SetUp]
        public void Setup()
        {
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null
            );

            _mockTokenRepo = new Mock<ITokenService>();

            _mockLoginService = new Mock<ILoginService>();
            _controller = new LoginController(_mockLoginService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (_controller is IDisposable disposable)
            {
                disposable.Dispose();
            }

            _controller = null;
        }

        [Test]
        public async Task Login_ValidCredentials_ReturnsOkResultWithToken()
        {
            // Arrange
            var loginRequest = new LoginRequestDto
            {
                Email = "admin@gmail.com",
                Password = "testing123"
            };

            var loginResponseDto = new LoginResponseDto
            {
                Email = "admin@gmail.com",
                JwtToken = "fake-jwt-token"
            };
            

            _mockLoginService
                .Setup(service => service.LoginAsync(loginRequest.Email, loginRequest.Password))
                .ReturnsAsync((loginResponseDto));

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            // Extract the token from the anonymous object
            var value = okResult?.Value as LoginResponseDto;
            Assert.That(value, Is.Not.Null);
            Assert.That(value.Email, Is.EqualTo(loginResponseDto.Email));
            Assert.That(value.JwtToken, Is.EqualTo(loginResponseDto.JwtToken));
        }


        [Test]
        public async Task Login_InvalidCredentials_ReturnsBadRequest()
        {
            var request = new LoginRequestDto
            {
                Email = "fake@example.com",
                Password = "wrongpassword"
            };

            _mockLoginService
                .Setup(service => service.LoginAsync(request.Email, request.Password))
                .ReturnsAsync((LoginResponseDto)null!);


            var result = await _controller.Login(request);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badResult = result as BadRequestObjectResult;
            Assert.That(badResult!.Value, Is.EqualTo("Invalid email or password"));
        }
    }
}
