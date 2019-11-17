using JetBrains.Annotations;

namespace Suitsupply.Tailoring.Web.Api.Configuration
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class TopicSubscriptionConfig
    {
        public string ServiceBusConnectionString { get; set; }
        public string TopicName { get; set; }
        public string SubscriptionName { get; set; }
        public int MaxConcurrentCall { get; set; }

        public int GetMaxConcurrentCall()
        {
            // Process concurrently only one message by default
            return MaxConcurrentCall > 0 ? MaxConcurrentCall : 1;
        }
    }
}