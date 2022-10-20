namespace SFA.DAS.Shortlist.Application.Data.Repositories
{
    public interface IShortlistRepository
    {
        Task<List<Domain.Entities.Shortlist>> GetAll(Guid userId);

        Task<int> GetCount(Guid userId);

        Task Insert(Domain.Entities.Shortlist entity);

        Task DeleteShortlistByUserId(Guid shortlistUserId);

        Task Delete(Guid id, Guid shortlistUserId);
    }
}
