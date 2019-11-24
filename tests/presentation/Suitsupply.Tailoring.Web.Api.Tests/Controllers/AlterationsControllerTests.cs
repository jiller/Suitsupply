using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Core.Cqrs;
using Suitsupply.Tailoring.Services.Alterations;
using Suitsupply.Tailoring.Web.Api.Controllers;
using Suitsupply.Tailoring.Web.Api.Controllers.Requests;

namespace Suitsupply.Tailoring.Web.Api.Tests.Controllers
{
    [TestFixture]
    public class AlterationsControllerTests
    {
        private Mock<ILogger<AlterationsController>> _logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = new Mock<ILogger<AlterationsController>>();
        }
        
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task PostShouldCallCreateAlterationCommand()
        {
            var request = new AlterationRequest
            {
                CustomerId = 123,
                ShortenSleeves = new Shortening(3, 0),
                ShortenTrousers = new Shortening(0, 2)
            };
            
            var command = new CreateAlterationCommand
            {
                Alteration = new NewAlteration
                {
                    ShortenSleevesLeft = request.ShortenSleeves.Left,
                    ShortenSleevesRight = request.ShortenSleeves.Right,
                    ShortenTrousersLeft = request.ShortenTrousers.Left,
                    ShortenTrousersRight = request.ShortenTrousers.Right,
                    CustomerId = request.CustomerId
                }
            };
            
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(m => m.Map<CreateAlterationCommand>(It.IsAny<AlterationRequest>()))
                .Returns(command);

            var controller = new AlterationsController(_logger.Object, mediatorMock.Object, mapperMock.Object);
            var result = await controller.Post(request);
            
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            mediatorMock
                .Verify(x => x.ExecuteAsync(
                        It.Is<CreateAlterationCommand>(c => c.Alteration.ShortenSleevesLeft == request.ShortenSleeves.Left &&
                                                            c.Alteration.ShortenTrousersRight == request.ShortenTrousers.Right)),
                    Times.Once);
        }
    }
}