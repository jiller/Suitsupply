﻿using System.Linq;
using FluentAssertions;
using FluentAssertions.Equivalency;
using NUnit.Framework;
using Suitsupply.Tailoring.Core.Cqrs;
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
                    ShortenSleevesLeft = 3,
                    ShortenTrousersRight = 2,
                    CustomerId = 123
                }
            };
            
            var handler = new CreateAlterationCommandHandler(GetDbContextFactory(), GetDateTimeProvider());

            IResult result = null;
            Assert.DoesNotThrowAsync(async () => { result = await handler.ExecuteAsync(command); });
            Assert.That(result.IsSuccess, Is.True);

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
                .Using<AlterationState>(ctx => ctx.Subject.Should().Be(AlterationState.Created))
                .WhenTypeIs<AlterationState>()
                .WithTracing();
        }
    }
}