using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Suitsupply.Tailoring.DataAccess
{
    [UsedImplicitly]
    public class TailoringDbContextFactory : Core.IDbContextFactory<TailoringDbContext>
    {
        private readonly DbContextOptions<TailoringDbContext> _options;

        public TailoringDbContextFactory(DbContextOptions<TailoringDbContext> options)
        {
            _options = options;
        }

        public TailoringDbContext Create()
        {
            return new TailoringDbContext(_options);
        }
    }
}