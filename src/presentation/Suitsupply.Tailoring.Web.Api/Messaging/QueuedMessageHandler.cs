using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.Services.Alterations;
using Suitsupply.Tailoring.Web.Api.Extensions;
using Suitsupply.Tailoring.Web.Api.Messaging.Converters;

namespace Suitsupply.Tailoring.Web.Api.Messaging
{
    [UsedImplicitly]
    public class QueuedMessageHandler : IQueuedMessageHandler
    {
        private readonly ILogger<QueuedMessageHandler> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public QueuedMessageHandler(
            ILogger<QueuedMessageHandler> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ProcessOrderPaidMessageAsync(Message message, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var commandHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<PayAlterationCommand, Alteration>>();
                await commandHandler.HandleAsync(message.ConvertToCommand<PayAlterationCommand>());
            }
        }

        public Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            _logger.LogError(arg.Exception, 
                $"Message handler encountered an exception. Context for troubleshooting:{Environment.NewLine}{arg.ExceptionReceivedContext.ToJson()}");

            return Task.CompletedTask;
        }
    }
}