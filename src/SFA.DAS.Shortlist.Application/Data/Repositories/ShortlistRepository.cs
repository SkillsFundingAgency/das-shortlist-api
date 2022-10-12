using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Shortlist.Application.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ShortlistRepository : IShortlistRepository
    {
        private readonly ShortlistDataContext _context;

        public ShortlistRepository(ShortlistDataContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Shortlist>> GetAll(Guid userId)
        {
            return await _context.Shortlists.Where(s => s.ShortlistUserId == userId).ToListAsync();
        }

        public async Task Insert(Domain.Entities.Shortlist entity)
        {
            _context.Shortlists.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
