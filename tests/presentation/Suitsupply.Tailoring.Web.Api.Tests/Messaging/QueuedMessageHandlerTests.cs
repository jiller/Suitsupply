using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Data;
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
            var commandHandlerMock = new Mock<ICommandHandler<PayAlterationCommand, Alteration>>();

            await CreateAndExecutePayAlterationCommandHandler(commandHandlerMock.Object, command);
            
            commandHandlerMock.Verify(h => h.HandleAsync(It.Is<PayAlterationCommand>(c => c.AlterationId == command.AlterationId)), Times.Once);
        }

        private async Task CreateAndExecutePayAlterationCommandHandler(ICommandHandler<PayAlterationCommand, Alteration>  commandHandler, PayAlterationCommand command)
        {
            var serviceProvider = BuildServiceProvider(commandHandler);
            
            var messageHandler = new QueuedMessageHandler(_loggerMock.Object, serviceProvider);

            await messageHandler.ProcessOrderPaidMessageAsync(
                new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(command))), CancellationToken.None);
        }

        private IServiceScopeFactory BuildServiceProvider(ICommandHandler<PayAlterationCommand, Alteration> commandHandler)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(x => commandHandler);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
            serviceScopeFactoryMock
                .Setup(f => f.CreateScope())
                .Returns(serviceProvider.CreateScope);
            
            return serviceScopeFactoryMock.Object;
        }
    }
}