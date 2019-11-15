namespace Suitsupply.Tailoring.Core
{
    public interface IDbContextFactory<out TDbContext>
    {
        TDbContext Create();
    }
}