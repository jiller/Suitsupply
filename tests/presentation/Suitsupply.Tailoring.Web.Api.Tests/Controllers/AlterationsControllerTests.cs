using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Services.Alterations;
using Suitsupply.Tailoring.Web.Api.Controllers;
using Suitsupply.Tailoring.Web.Api.Requests;

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
        public void PostShouldCallCreateAlterationCommand()
        {
            var handler = new Mock<ICommandHandler<CreateAlterationCommand, NewAlteration>>();
            var controller = new AlterationsController(_logger.Object, handler.Object);

            var request = new AlterationRequest
            {
                ShortenSleeves = 3,
                ShortenTrousers = 2
            };

            var result = controller.Post(request);
            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            
            handler
                .Verify(x => x.HandleAsync(
                        It.Is<CreateAlterationCommand>(c => c.Alteration.ShortenSleeves == request.ShortenSleeves &&
                                                            c.Alteration.ShortenTrousers == request.ShortenTrousers)),
                    Times.Once);
        }
    }
}