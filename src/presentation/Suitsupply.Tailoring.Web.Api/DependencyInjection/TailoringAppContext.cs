using System;
using JetBrains.Annotations;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.DataAccess;
using Suitsupply.Tailoring.Web.Api.Configuration;

namespace Suitsupply.Tailoring.Web.Api.DependencyInjection
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class TailoringAppContext : IDisposable
    {
        public TailoringAppContext(IConfiguration configuration)
        {
            Configuration = configuration;
            Container = InitializeAndVerifyContainer();
        }

        public IConfiguration Configuration { get; }
        public Container Container { get; }

        private Container InitializeAndVerifyContainer()
        {
            var container = new Container();
            container.Options.AllowOverridingRegistrations = true;
            
            var topicSubscriptionConfig = Configuration.GetSection($"{nameof(TopicSubscriptionConfig)}").Get<TopicSubscriptionConfig>();
            container.RegisterInstance(topicSubscriptionConfig);
            
            container.Register(typeof(IHandler<,>), AppDomain.CurrentDomain.GetAssemblies());
            
            container.Register<ISubscriptionClient>(() =>
            {
                var config = container.GetInstance<TopicSubscriptionConfig>();
                
                return new SubscriptionClient(
                    config.ServiceBusConnectionString,
                    config.TopicName,
                    config.SubscriptionName);
            }, Lifestyle.Singleton);
            
            container.Register(() =>
            {
                var connectionString = Configuration.GetConnectionString("tailoring-db");
                var optionsBuilder = new DbContextOptionsBuilder<TailoringDbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                return optionsBuilder.Options;
            }, Lifestyle.Singleton);
            container.Register(typeof(IDbContextFactory<TailoringDbContext>), typeof(TailoringDbContextFactory));

            return container;
        }
        
        private void ReleaseUnmanagedResources()
        {
            Container.Dispose();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~TailoringAppContext()
        {
            ReleaseUnmanagedResources();
        }
    }
}