using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SFA.DAS.Shortlist.Api.Controllers;
using SFA.DAS.Shortlist.Application.Services;

namespace SFA.DAS.Shortlist.Api.UnitTests
{
    [TestFixture]
    public class ShortlistControllerTests
    {
        [Test]
        public async Task CreateShortListItem_InvalidRequest_ReturnsBadRequestResponse()
        {
            var loggerMock = new Mock<ILogger<ShortlistController>>();
            var serviceMock = new Mock<IShortlistService>();
            var sut = new ShortlistController(loggerMock.Object, serviceMock.Object);
            sut.ModelState.AddModelError("key", "error");

            var result = await sut.CreateShortlistItem(new Models.ShortlistAddModel());

            result.As<BadRequestObjectResult>().Should().NotBeNull();
        }

        [Test]
        public async Task CreateShortListItem_ValidRequest_ReturnsCreatedAtResponse()
        {
            var loggerMock = new Mock<ILogger<ShortlistController>>();
            var serviceMock = new Mock<IShortlistService>();
            var model = new Models.ShortlistAddModel();
            var sut = new ShortlistController(loggerMock.Object, serviceMock.Object);

            var result = await sut.CreateShortlistItem(model);

            result.As<CreatedAtActionResult>().Should().NotBeNull();
            result.As<CreatedAtActionResult>().ActionName.Should().Be(nameof(ShortlistController.GetAllForUser));
            serviceMock.Verify(x => x.AddItem(It.IsAny<Application.Domain.Entities.Shortlist>()));
        }

        [Test]
        public async Task GetAllForUser_ValidRequest_ReturnsShortlists()
        {
            var loggerMock = new Mock<ILogger<ShortlistController>>();
            var serviceMock = new Mock<IShortlistService>();
            var userId = Guid.NewGuid();
            var sut = new ShortlistController(loggerMock.Object, serviceMock.Object);


            var result = await sut.GetAllForUser(userId);

            result.As<OkObjectResult>().Should().NotBeNull();
            serviceMock.Verify(x => x.GetAllUserShortlist(userId));
        }

        [Test]
        public async Task DeleteShortlistForUser_ReturnsNoContentResponse()
        {
            var loggerMock = new Mock<ILogger<ShortlistController>>();
            var serviceMock = new Mock<IShortlistService>();
            var sut = new ShortlistController(loggerMock.Object, serviceMock.Object);
            var userId = Guid.NewGuid();

            var result = await sut.DeleteShortlistForUser(userId);

            result.As<NoContentResult>().Should().NotBeNull();
            serviceMock.Verify(x => x.DeleteAllShortlistForUser(userId));
        }
    }
}
