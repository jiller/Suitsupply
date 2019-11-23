using System;
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
            
            var commandHandler = new PayAlterationCommandHandler(GetDbContextFactory());
            var result = await commandHandler.HandleAsync(command);
            
            Assert.That(result.State, Is.EqualTo(AlterationState.Paid));
            Assert.That(result.PayDate, Is.Not.Null);
        }
        
        [Test]
        public async Task ShouldDoNothingOnPaidAlterationAsync()
        {
            DateTime payDate = DateTime.UtcNow.AddDays(-1);
            var command = new PayAlterationCommand
            {
                AlterationId = BootstrapDbContextWithPaidAlteration(payDate)
            };
            
            var commandHandler = new PayAlterationCommandHandler(GetDbContextFactory());
            var result = await commandHandler.HandleAsync(command);
            
            Assert.That(result.State, Is.EqualTo(AlterationState.Paid));
            Assert.That(result.PayDate, Is.EqualTo(payDate));
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