using System;
using System.Linq;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using NUnit.Framework;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.Services.Alterations;

namespace Suitsupply.Tailoring.Services.Tests.Alterations
{
    [TestFixture]
    public class PayAlterationCommandHandlerTests : BaseCommandHandlerTests
    {
        [Test]
        public async Task ShouldPayCreatedAlterationAsync()
        {
            var command = new PayAlterationCommand
            {
                AlterationId = BootstrapDbContextWithCreatedAlteration()
            };
            var utcNow = SetUpCurrentDateTime(DateTime.UtcNow);
            
            var commandHandler = CreatePayAlterationCommandHandler();
            var result = await commandHandler.ExecuteAsync(command);
            
            Assert.That(result.IsSuccess, Is.True);

            using (var db = GetDbContextFactory().Create())
            {
                var alteration = db.Alterations.FirstOrDefault(a => a.Id == command.AlterationId);
                
                Assert.That(alteration.State, Is.EqualTo(AlterationState.Paid));
                Assert.That(alteration.PayDate, Is.Not.Null);
                Assert.That(alteration.PayDate, Is.EqualTo(utcNow));
            }
        }

        [Test]
        public async Task ShouldDoNothingOnPaidAlterationAsync()
        {
            var payDate = DateTime.UtcNow.AddDays(-1);
            var command = new PayAlterationCommand
            {
                AlterationId = BootstrapDbContextWithPaidAlteration(payDate)
            };

            var commandHandler = CreatePayAlterationCommandHandler();
            var result = await commandHandler.ExecuteAsync(command);
            
            using (var db = GetDbContextFactory().Create())
            {
                var alteration = db.Alterations.FirstOrDefault(a => a.Id == command.AlterationId);
                
                Assert.That(alteration.State, Is.EqualTo(AlterationState.Paid));
                Assert.That(alteration.PayDate, Is.Not.Null);
                Assert.That(alteration.PayDate, Is.EqualTo(payDate));
            }
        }
        
        private PayAlterationCommandHandler CreatePayAlterationCommandHandler()
        {
            var commandHandler = new PayAlterationCommandHandler(
                GetDbContextFactory(),
                GetDateTimeProvider());
            return commandHandler;
        }

        private int BootstrapDbContextWithPaidAlteration(DateTime payDate)
        {
            using (var db = GetDbContextFactory().Create())
            {
                var alteration = Builder<Alteration>.CreateNew()
                    .With(a => a.State, AlterationState.Paid)
                    .With(a => a.PayDate, payDate)
                    .Build();

                db.Alterations.Add(alteration);
                db.SaveChanges();
                return alteration.Id;
            }
        }

        private int BootstrapDbContextWithCreatedAlteration()
        {
            using (var db = GetDbContextFactory().Create())
            {
                var alteration = Builder<Alteration>.CreateNew()
                    .With(a => a.State, AlterationState.Created)
                    .Build();
                
                db.Alterations.Add(alteration);
                db.SaveChanges();
                return alteration.Id;
            }
        }
    }
}