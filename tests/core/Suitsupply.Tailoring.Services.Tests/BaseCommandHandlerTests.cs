using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.DataAccess;

namespace Suitsupply.Tailoring.Services.Tests
{
    public class BaseCommandHandlerTests
    {
        private readonly DbContextOptions<TailoringDbContext> _options;
        private readonly Mock<IDbContextFactory<TailoringDbContext>> _dbFactory;

        protected BaseCommandHandlerTests()
        {
            _options = new DbContextOptionsBuilder<TailoringDbContext>()
                .UseInMemoryDatabase(databaseName: "tailoring_db")
                .Options;
            
            _dbFactory = new Mock<IDbContextFactory<TailoringDbContext>>();
            _dbFactory.Setup(x => x.Create()).Returns(() => new TailoringDbContext(_options));
        }

        protected IDbContextFactory<TailoringDbContext> GetDbContextFactory()
        {
            return _dbFactory.Object;
        }
        
        [SetUp]
        public virtual void SetUp()
        {
            // Clear in-memory database between tests
            using (var db = GetDbContextFactory().Create())
            {
                db.Database.EnsureDeleted();
            }
        }
    }
}