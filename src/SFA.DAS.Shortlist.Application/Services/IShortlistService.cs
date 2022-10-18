namespace SFA.DAS.Shortlist.Application.Services
{
    public interface IShortlistService
    {
        Task<List<Domain.Entities.Shortlist>> GetAllUserShortlist(Guid userId);
        Task AddItem(Domain.Entities.Shortlist shortlist);
    }
}
