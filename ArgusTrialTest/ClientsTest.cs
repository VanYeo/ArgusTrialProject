using backend.Controllers;
using backend.DTOs.Dashboard;
using backend.Entities;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgusTrialTest
{
    internal class ClientsTest
    {
        private Mock<IClientsRepository> _mockRepo;
        private ClientsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IClientsRepository>();
            _controller = new ClientsController(_mockRepo.Object);
        }

        [Test]
        public async Task SearchClients_ValidRequest_ReturnsOkWithResults()
        {
            // Arrange
            var request = new SearchRequestDto { Keyword = "test", PageIndex = 1, PageSize = 10 };
            var expectedResult = new PaginatorDto<SearchResponseDto>
            {
                Items = new List<SearchResponseDto>(),
                PageIndex = 1,
                PageSize = 10,
                TotalCount = 0,
                TotalPages = 0,
                HasNextPage = false,
                HasPreviousPage = false
            };

            _mockRepo.Setup(r => r.GetClientsAsync(request)).ReturnsAsync(expectedResult);

            var result = await _controller.SearchClients(request);

            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task CreateClient_ValidClient_ReturnsOk()
        {
            var client = new Client { CompanyName = "New Co" };
            _mockRepo.Setup(r => r.AddClientAsync(client)).ReturnsAsync(client);

            var result = await _controller.CreateClient(client);

            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        [Test]
        public async Task UpdateClient_IdMismatch_ReturnsBadRequest()
        {
            var client = new Client { ClientID = 2, CompanyName = "Mismatch Co" };

            var result = await _controller.UpdateClient(1, client);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateClient_ClientDoesNotExist_ReturnsBadRequest()
        {
            var client = new Client { ClientID = 1 };
            _mockRepo.Setup(r => r.ClientExists(1)).Returns(false);
            
            var result = await _controller.UpdateClient(1, client);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task UpdateClient_ValidUpdate_ReturnsNoContent()
        {
            var client = new Client { ClientID = 1 };
            _mockRepo.Setup(r => r.ClientExists(1)).Returns(true);
            _mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

            var result = await _controller.UpdateClient(1, client);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task UpdateClient_SaveFails_ReturnsBadRequest()
        {
            var client = new Client { ClientID = 1 };
            _mockRepo.Setup(r => r.ClientExists(1)).Returns(true);
            _mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(false);

            var result = await _controller.UpdateClient(1, client);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
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
    }
}
