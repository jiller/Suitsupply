using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suitsupply.Tailoring.Data;

namespace Suitsupply.Tailoring.DataAccess.Configurations
{
    public class AlterationConfiguration : IEntityTypeConfiguration<Alteration>
    {
        public void Configure(EntityTypeBuilder<Alteration> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(a => a.ShortenSleevesLeft).IsRequired();
            builder.Property(a => a.ShortenSleevesRight).IsRequired();
            builder.Property(a => a.ShortenTrousersLeft).IsRequired();
            builder.Property(a => a.ShortenTrousersRight).IsRequired();
            builder.Property(a => a.CustomerId).IsRequired();
        }
    }
}