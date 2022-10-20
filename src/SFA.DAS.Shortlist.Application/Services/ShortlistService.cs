using SFA.DAS.Shortlist.Application.Data.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Shortlist.Application.Services
{
    [ExcludeFromCodeCoverage]
    public class ShortlistService : IShortlistService
    {
        private readonly IShortlistRepository _shortlistRepository;

        public ShortlistService(IShortlistRepository shortlistRepository)
        {
            _shortlistRepository = shortlistRepository;
        }

        public Task AddItem(Domain.Entities.Shortlist shortlist)
        {
            return _shortlistRepository.Insert(shortlist);
        }

        public Task<int> GetShortlistCountForUser(Guid userId)
        {
            return _shortlistRepository.GetCount(userId);
        }

        public Task<List<Domain.Entities.Shortlist>> GetAllUserShortlist(Guid userId)
        {
            return _shortlistRepository.GetAll(userId);
        }
        public async Task DeleteAllShortlistForUser(Guid shortlistUserId)
        {
            await _shortlistRepository.DeleteShortlistByUserId(shortlistUserId);
        }
        public Task DeleteShortlistUserItem(Guid id, Guid userId)
        {
            return _shortlistRepository.Delete(id, userId);
        }
    }
}
