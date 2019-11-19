using System.Collections.Generic;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using SimpleInjector;
using Suitsupply.Tailoring.Web.Api.DependencyInjection;

namespace Suitsupply.Tailoring.Web.Api.Tests.DependencyInjection
{
    [TestFixture]
    public class TailoringAppContextTests
    {
        private IConfigurationRoot _configuration;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"TopicSubscriptionConfig:ServiceBusConnectionString", "ServiceBusConnectionString"},
                    {"TopicSubscriptionConfig:TopicName", "OrderPaid"},
                    {"TopicSubscriptionConfig:SubscriptionName", "Suitsupply.Tailoring"},
                    {"ConnectionStrings:tailoring-db", "Data Source=localhost"}
                })
                .Build();
        }
        
        [Test]
        public void ContainerVerificationShouldNotThrowException()
        {
            using (var context = new TailoringAppContext(_configuration))
            {
                // Replace external dependencies in the container
                context.Container.Register(() => new Mock<ISubscriptionClient>().Object, Lifestyle.Singleton);
                
                Assert.DoesNotThrow(() => context.Container.Verify());
            }
        }
    }
}