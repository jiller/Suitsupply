using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Suitsupply.Tailoring.Web.Api.Extensions;

namespace Suitsupply.Tailoring.Web.Api.Messaging
{
    [UsedImplicitly]
    public class QueuedMessageHandler : IQueuedMessageHandler
    {
        private readonly ILogger<QueuedMessageHandler> _logger;
        private readonly IServiceProvider _serviceProvider;

        public QueuedMessageHandler(
            ILogger<QueuedMessageHandler> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task ProcessOrderPaidMessageAsync(Message message, CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                
            }
            
            return Task.CompletedTask;
        }

        public Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            _logger.LogError(arg.Exception, 
                $"Message handler encountered an exception. Context for troubleshooting:{Environment.NewLine}{arg.ExceptionReceivedContext.ToJson()}");

            return Task.CompletedTask;
        }
    }
}