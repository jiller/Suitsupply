using Microsoft.EntityFrameworkCore;
using Suitsupply.Tailoring.Data;

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
    }
}