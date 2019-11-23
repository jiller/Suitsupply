using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Suitsupply.Tailoring.Core.Cqrs;
using Suitsupply.Tailoring.Services.Alterations;
using Suitsupply.Tailoring.Web.Api.Messaging;

namespace Suitsupply.Tailoring.Web.Api.Tests.Messaging
{
    [TestFixture]
    public class QueuedMessageHandlerTests
    {
        private Mock<ILogger<QueuedMessageHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<QueuedMessageHandler>>();
        }
        
        [Test]
        public async Task ProcessOrderPaidMessageShouldExecuteRightCommand()
        {
            var command = new PayAlterationCommand
            {
                AlterationId = 123
            };
            var mediatorMock = new Mock<IMediator>();

            await CreateAndExecutePayAlterationCommandHandler(mediatorMock.Object, command);
            
            mediatorMock.Verify(h => h.ExecuteAsync(It.Is<PayAlterationCommand>(c => c.AlterationId == command.AlterationId)), Times.Once);
        }

        private async Task CreateAndExecutePayAlterationCommandHandler(IMediator mediator, PayAlterationCommand command)
        {
            var serviceProvider = BuildServiceProvider(mediator);
            
            var messageHandler = new QueuedMessageHandler(_loggerMock.Object, serviceProvider);

            await messageHandler.ProcessOrderPaidMessageAsync(
                new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(command))), CancellationToken.None);
        }

        private IServiceScopeFactory BuildServiceProvider(IMediator mediator)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(x => mediator);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
            serviceScopeFactoryMock
                .Setup(f => f.CreateScope())
                .Returns(serviceProvider.CreateScope);
            
            return serviceScopeFactoryMock.Object;
        }
    }
}