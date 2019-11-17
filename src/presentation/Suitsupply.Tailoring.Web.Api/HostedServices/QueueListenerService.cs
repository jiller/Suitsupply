using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.InteropExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Suitsupply.Tailoring.Web.Api.Configuration;
using Suitsupply.Tailoring.Web.Api.Extensions;

namespace Suitsupply.Tailoring.Web.Api.HostedServices
{
    [UsedImplicitly]
    public class QueueListenerService : IHostedService
    {
        private readonly ILogger<QueueListenerService> _logger;
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly TopicSubscriptionConfig _config;
        private readonly IServiceProvider _services;

        public QueueListenerService(
            ILogger<QueueListenerService> logger,
            ISubscriptionClient subscriptionClient,
            TopicSubscriptionConfig config,
            IServiceProvider services)
        {
            _logger = logger;
            _subscriptionClient = subscriptionClient;
            _config = config;
            _services = services;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var options = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = _config.MaxConcurrentCall
            };
            _subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, options);
        }

        private Task ProcessMessageAsync(Message message, CancellationToken cancellationToken)
        {
            using (var scope = _services.CreateScope())
            {
                throw new NotImplementedException();
            }
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            _logger.LogError(arg.Exception, 
                $"Message handler encountered an exception. Context for troubleshooting:{Environment.NewLine}{arg.ExceptionReceivedContext.ToJson()}");

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _subscriptionClient?.CloseAsync();
        }
    }
}