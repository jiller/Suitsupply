using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.DataAccess;
using Suitsupply.Tailoring.Services.Alterations;

namespace Suitsupply.Tailoring.Services.Tests.Alterations
{
    [TestFixture]
    public class CreateAlterationCommandHandlerTests
    {
        private DbContextOptions<TailoringDbContext> _options;
        private TailoringDbContext _dbContext;
        private Mock<IDbContextFactory<TailoringDbContext>> _dbFactory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _options = new DbContextOptionsBuilder<TailoringDbContext>()
                .UseInMemoryDatabase(databaseName: "tailoring_db")
                .Options;

            _dbFactory = new Mock<IDbContextFactory<TailoringDbContext>>();
            _dbFactory.Setup(x => x.Create()).Returns(() => _dbContext);
        }

        [SetUp]
        public void SetUp()
        {
            _dbContext = new TailoringDbContext(_options);
        }

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
            
            var handler = new CreateAlterationCommandHandler(_dbFactory.Object);

            NewAlteration result = null;
            Assert.DoesNotThrow(() => { result = handler.Handle(command); });
            
            command.Alteration
                .Should()
                .BeEquivalentTo(result, options => options.Excluding(x => x.Id));

            using (var db = new TailoringDbContext(_options))
            {
                var alterations = db.Alterations.ToArray();

                new[] {command.Alteration}
                    .Should()
                    .BeEquivalentTo(alterations, options => options.Excluding(x => x.Id));
            }
        }
    }
}