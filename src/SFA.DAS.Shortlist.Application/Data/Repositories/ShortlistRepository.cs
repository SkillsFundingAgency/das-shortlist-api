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
            return await _context.Shortlists.AsNoTracking().Where(s => s.ShortlistUserId == userId).ToListAsync();
        }

        public async Task<int> GetCount(Guid userId)
        {
            return await _context.Shortlists.AsNoTracking().Where(s => s.ShortlistUserId == userId).CountAsync();
        }


        public async Task Insert(Domain.Entities.Shortlist entity)
        {
            _context.Shortlists.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShortlistByUserId(Guid shortlistUserId)
        {
            var shortListItemsToDelete =
                await _context.Shortlists.Where(x => x.ShortlistUserId == shortlistUserId).ToListAsync();

            _context.Shortlists.RemoveRange(shortListItemsToDelete);

            _context.SaveChanges();
        }

        public async Task<List<Guid>> GetExpiredShortlistUserIds(int expiryInDays)
        {
            return await _context
                .Shortlists
                .AsNoTracking()
                .GroupBy(item => item.ShortlistUserId)
                .Select(c => new
                {
                    shortListUserId = c.Key,
                    latestCreatedDate = c.Max(x => x.CreatedDate)
                })
                .Where(shortlistUser => shortlistUser.latestCreatedDate.AddDays(expiryInDays) < DateTime.UtcNow)
                .Select(c => c.shortListUserId)
                .ToListAsync();
        }
    }
}
