using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Shortlist.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class ShortlistDataContext : DbContext
    {
        public DbSet<Domain.Entities.Shortlist> Shortlists { get; set; }

        public ShortlistDataContext(DbContextOptions<ShortlistDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Domain.Entities.Shortlist>(new ShortlistConfiguration());
        }
    }

    public class ShortlistConfiguration : IEntityTypeConfiguration<Domain.Entities.Shortlist>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Shortlist> builder)
        {
            builder.ToTable("Shortlist");
            builder.Property(s => s.Latitude).HasColumnType("float");
            builder.Property(s => s.Latitude).HasColumnType("float");
        }
    }
}
