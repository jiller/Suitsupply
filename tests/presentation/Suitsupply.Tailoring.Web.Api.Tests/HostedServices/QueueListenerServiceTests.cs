using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Suitsupply.Tailoring.Web.Api.Configuration;
using Suitsupply.Tailoring.Web.Api.HostedServices;

namespace Suitsupply.Tailoring.Web.Api.Tests.HostedServices
{
    [TestFixture]
    public class QueueListenerServiceTests
    {
        private Mock<ILogger<QueueListenerService>> _loggerMock;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _loggerMock = new Mock<ILogger<QueueListenerService>>();
        }
        
        [Test]
        public async Task SubscriptionClientShouldDisposeOnStopping()
        {
            var subscriptionClientMock = new Mock<ISubscriptionClient>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            
            var service = new QueueListenerService(
                _loggerMock.Object,
                subscriptionClientMock.Object,
                new TopicSubscriptionConfig(), 
                serviceProviderMock.Object);

            await service.StopAsync(CancellationToken.None);
            
            subscriptionClientMock
                .Verify(s => s.CloseAsync(), Times.Once);
        }
    }
}