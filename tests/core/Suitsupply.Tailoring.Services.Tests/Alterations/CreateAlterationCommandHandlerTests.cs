using System.Linq;
using FluentAssertions;
using FluentAssertions.Equivalency;
using NUnit.Framework;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.Services.Alterations;

namespace Suitsupply.Tailoring.Services.Tests.Alterations
{
    [TestFixture]
    public class CreateAlterationCommandHandlerTests : BaseCommandHandlerTests
    {
        [Test]
        public void CreateAlterationCommandShouldBeHandled()
        {
            var command = new CreateAlterationCommand
            {
                Alteration = new NewAlteration
                {
                    ShortenSleeves = 3,
                    ShortenTrousers = 2,
                    CustomerId = 123
                }
            };
            
            var handler = new CreateAlterationCommandHandler(GetDbContextFactory());

            NewAlteration result = null;
            Assert.DoesNotThrowAsync(async () => { result = await handler.HandleAsync(command); });
            
            command.Alteration
                .Should()
                .BeEquivalentTo(result, options => options.Excluding(x => x.Id));

            using (var db = GetDbContextFactory().Create())
            {
                var alterations = db.Alterations.ToArray();

                alterations
                    .Should()
                    .BeEquivalentTo(new[] {command.Alteration}, GetNewAlterationComparerOptions);
            }
        }

        private static EquivalencyAssertionOptions<NewAlteration> GetNewAlterationComparerOptions(EquivalencyAssertionOptions<NewAlteration> options)
        {
            return options
                .Excluding(x => x.Id)
                .Using<AlterationState>(ctx => ctx.Subject.Should().Be(AlterationState.Created))
                .WhenTypeIs<AlterationState>()
                .WithTracing();
        }
    }
}