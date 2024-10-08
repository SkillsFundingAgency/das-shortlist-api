﻿namespace SFA.DAS.Shortlist.Application.Services
{
    public interface IShortlistService
    {
        Task<List<Domain.Entities.Shortlist>> GetAllUserShortlist(Guid userId);
        Task<int> GetShortlistCountForUser(Guid userId);
        Task AddItem(Domain.Entities.Shortlist shortlist);
        Task DeleteAllShortlistForUser(Guid shortlistUserId);
        Task<List<Guid>> GetExpiredShortlistUserIds(int expiryInDays);
        Task DeleteShortlistUserItem(Guid id, Guid userId);
    }
}
