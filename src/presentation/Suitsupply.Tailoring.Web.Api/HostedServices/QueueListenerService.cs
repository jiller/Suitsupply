using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Suitsupply.Tailoring.Web.Api.Configuration;
using Suitsupply.Tailoring.Web.Api.Messaging;

namespace Suitsupply.Tailoring.Web.Api.HostedServices
{
    [UsedImplicitly]
    public class QueueListenerService : IQueueListenerService
    {
        private readonly ILogger<QueueListenerService> _logger;
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly TopicSubscriptionConfig _config;
        private readonly IQueuedMessageHandler _messageHandler;

        public QueueListenerService(
            ILogger<QueueListenerService> logger,
            ISubscriptionClient subscriptionClient,
            TopicSubscriptionConfig config,
            IQueuedMessageHandler messageHandler)
        {
            _logger = logger;
            _subscriptionClient = subscriptionClient;
            _config = config;
            _messageHandler = messageHandler;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var options = new MessageHandlerOptions(_messageHandler.ExceptionReceivedHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = _config.MaxConcurrentCall
            };
            _subscriptionClient.RegisterMessageHandler(ProcessOrderPaidMessageAsync, options);
            
            _logger.LogInformation($"Background {nameof(QueueListenerService)} is started");
            return Task.CompletedTask;
        }
        
        public async Task ProcessOrderPaidMessageAsync(Message message, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope($"[CorrelationId: {message.CorrelationId}; LockToken: {message.SystemProperties.LockToken}]"))
            {
                _logger.LogInformation("Message was received");
                try
                {
                    await _messageHandler.ProcessOrderPaidMessageAsync(message, cancellationToken);

                    _logger.LogInformation("Message was successfully processed");
                    await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                }
                catch (Exception err)
                {
                    _logger.LogError(err, "Error occured while processing message");
                    await _subscriptionClient.AbandonAsync(message.SystemProperties.LockToken);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stopping background {nameof(QueueListenerService)}...");
            await _subscriptionClient?.CloseAsync();
            _logger.LogInformation($"Background {nameof(QueueListenerService)} is stopped");
        }
    }
}