using System;
using System.Linq;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Core.Cqrs;
using Suitsupply.Tailoring.DataAccess;
using Suitsupply.Tailoring.Services;
using Suitsupply.Tailoring.Web.Api.Configuration;
using Suitsupply.Tailoring.Web.Api.Mappings;
using Suitsupply.Tailoring.Web.Api.Messaging;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Suitsupply.Tailoring.Web.Api.DependencyInjection
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class TailoringAppContext : IDisposable
    {
        public TailoringAppContext(IConfiguration configuration)
        {
            Configuration = configuration;
            Container = new Container();
        }

        public IConfiguration Configuration { get; }
        public Container Container { get; }

        public void InitializeContainer()
        {
            Container.Options.AllowOverridingRegistrations = true;
            
            RegisterServiceBus();
            RegisterDatabase();
            RegisterCqs();
            RegisterAutoMapper();
            
            Container.RegisterSingleton<INotificationService, NotificationService>();
            Container.RegisterSingleton<IDateTimeProvider, DateTimeProvider>();
            Container.RegisterSingleton<IQueuedMessageHandler>(() =>
            {
                return new QueuedMessageHandler(
                    Container.GetInstance<ILogger<QueuedMessageHandler>>(),
                    Container.GetInstance<IServiceScopeFactory>());
            });
        }

        private void RegisterAutoMapper()
        {
            Container.RegisterSingleton(() =>
            {
                var mp = Container.GetInstance<MapperProvider>();
                return mp.GetMapper();
            });
        }

        private void RegisterServiceBus()
        {
            var topicSubscriptionConfig = Configuration.GetSection($"{nameof(TopicSubscriptionConfig)}").Get<TopicSubscriptionConfig>();
            Container.RegisterInstance(topicSubscriptionConfig);

            Container.Register<ISubscriptionClient>(() =>
            {
                var config = Container.GetInstance<TopicSubscriptionConfig>();

                return new SubscriptionClient(
                    config.ServiceBusConnectionString,
                    config.TopicName,
                    config.SubscriptionName);
            }, Lifestyle.Singleton);
        }

        private void RegisterDatabase()
        {
            Container.Register(() =>
            {
                var connectionString = Configuration.GetConnectionString("tailoring-db");
                var optionsBuilder = new DbContextOptionsBuilder<TailoringDbContext>();
                //optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.UseInMemoryDatabase();
                return optionsBuilder.Options;
            }, Lifestyle.Singleton);
            Container.Register(typeof(IDbContextFactory<TailoringDbContext>), typeof(TailoringDbContextFactory));
        }

        private void RegisterCqs()
        {
            var servicesAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .First(a => a.GetName().Name == "Suitsupply.Tailoring.Services");

            Container.Register(typeof(ICommandHandler<>), servicesAssembly);
            Container.Register(typeof(IQueryHandler<,>), servicesAssembly);
            Container.RegisterSingleton<IMediator>(() => new Mediator(type => Container.GetInstance(type)));
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