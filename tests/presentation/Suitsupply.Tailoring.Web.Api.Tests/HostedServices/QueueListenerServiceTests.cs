using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Suitsupply.Tailoring.Web.Api.Configuration;
using Suitsupply.Tailoring.Web.Api.HostedServices;
using Suitsupply.Tailoring.Web.Api.Messaging;

namespace Suitsupply.Tailoring.Web.Api.Tests.HostedServices
{
    [TestFixture]
    public class QueueListenerServiceTests
    {
        private Mock<ILogger<QueueListenerService>> _loggerMock;
        private Mock<ISubscriptionClient> _subscriptionClientMock;
        private Mock<IQueuedMessageHandler> _messageHandlerMock;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _loggerMock = new Mock<ILogger<QueueListenerService>>();
        }

        [SetUp]
        public void SetUp()
        {
            _subscriptionClientMock = new Mock<ISubscriptionClient>();
            _messageHandlerMock = new Mock<IQueuedMessageHandler>();
        }
        
        [Test]
        public async Task SubscriptionClientShouldDisposeOnStopping()
        {
            var service = CreateQueueListenerService();

            await service.StopAsync(CancellationToken.None);
            
            _subscriptionClientMock.Verify(s => s.CloseAsync(), Times.Once);
        }

        private QueueListenerService CreateQueueListenerService(
            ILogger<QueueListenerService> logger = null,
            ISubscriptionClient subscriptionClient = null,
            TopicSubscriptionConfig config = null,
            IQueuedMessageHandler messageHandler = null)
        {
            return new QueueListenerService(
                logger ?? _loggerMock.Object,
                subscriptionClient ?? _subscriptionClientMock.Object,
                config ?? new TopicSubscriptionConfig(), 
                messageHandler ?? _messageHandlerMock.Object);
        }
    }
}