using Microsoft.EntityFrameworkCore;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.DataAccess.Configurations;

namespace Suitsupply.Tailoring.DataAccess
{
    public class TailoringDbContext : DbContext
    {
        public TailoringDbContext()
        {
        }

        public TailoringDbContext(DbContextOptions<TailoringDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Alteration> Alterations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AlterationConfiguration());
        }
    }
}