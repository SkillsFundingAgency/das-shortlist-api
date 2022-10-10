using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.Shortlist.Application.Data
{
    public class ShortlistDataContext : DbContext
    {
        public DbSet<Domain.Entities.Shortlist> Shortlists { get; set; }
    }
}
