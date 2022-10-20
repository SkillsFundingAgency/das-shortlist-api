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

        public async Task<int> GetCount(Guid userId)
        {
            return await _context.Shortlists.Where(s => s.ShortlistUserId == userId).CountAsync();
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
        public async Task Delete(Guid id, Guid shortlistUserId)
        {
            var shortlistItem =
                _context.Shortlists.SingleOrDefault(c => c.Id.Equals(id) && c.ShortlistUserId.Equals(shortlistUserId));
            if (shortlistItem != null)
            {
                _context.Shortlists.Remove(shortlistItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
