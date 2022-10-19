﻿using SFA.DAS.Shortlist.Application.Data.Repositories;
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

        public Task<List<Domain.Entities.Shortlist>> GetAllUserShortlist(Guid userId)
        {
            return _shortlistRepository.GetAll(userId);
        }
        public Task DeleteShortlistUserItem(Guid id, Guid userId)
        {
            return _shortlistRepository.Delete(id, userId);
        }
    }
}
