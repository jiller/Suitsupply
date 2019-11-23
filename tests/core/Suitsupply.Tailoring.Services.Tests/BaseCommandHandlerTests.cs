using System;
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
        private readonly Mock<IDbContextFactory<TailoringDbContext>> _dbFactoryMock;
        private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

        protected BaseCommandHandlerTests()
        {
            _options = new DbContextOptionsBuilder<TailoringDbContext>()
                .UseInMemoryDatabase(databaseName: "tailoring_db")
                .Options;
            
            _dbFactoryMock = new Mock<IDbContextFactory<TailoringDbContext>>();
            _dbFactoryMock.Setup(x => x.Create()).Returns(() => new TailoringDbContext(_options));
            
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        }

        protected IDbContextFactory<TailoringDbContext> GetDbContextFactory()
        {
            return _dbFactoryMock.Object;
        }

        protected IDateTimeProvider GetDateTimeProvider()
        {
            return _dateTimeProviderMock.Object;
        }
        
        [SetUp]
        public virtual void SetUp()
        {
            // Clear in-memory database between tests
            using (var db = GetDbContextFactory().Create())
            {
                db.Database.EnsureDeleted();
            }

            _dateTimeProviderMock.Setup(p => p.GetUtcNow()).Returns(DateTime.UtcNow);
        }

        protected DateTime SetUpCurrentDateTime(DateTime dateTime)
        {
            _dateTimeProviderMock.Setup(p => p.GetUtcNow()).Returns(dateTime);
            return dateTime;
        }
    }
}