namespace SFA.DAS.Shortlist.Application.Data.Repositories
{
    public interface IShortlistRepository
    {
        Task<List<Domain.Entities.Shortlist>> GetAll(Guid userId);

        Task<int> GetCount(Guid userId);

        Task Insert(Domain.Entities.Shortlist entity);

        Task DeleteShortlistByUserId(Guid shortlistUserId);

        Task<IEnumerable<Guid>> GetExpiredShortlistUserIds(int expiryInDays);
    }
}
