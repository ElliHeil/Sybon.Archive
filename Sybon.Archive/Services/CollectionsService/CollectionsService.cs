﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Sybon.Archive.Repositories.CollectionsRepository;
using Sybon.Archive.Services.CollectionsService.Models;
using Sybon.Archive.Services.InternalProblemsService;
using Sybon.Common;

namespace Sybon.Archive.Services.CollectionsService
{
    [UsedImplicitly]
    public class CollectionsService : ICollectionsService
    {
        private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IInternalProblemsService _internalProblemsService;
        
        public CollectionsService(IRepositoryUnitOfWork repositoryUnitOfWork, IMapper mapper, IInternalProblemsService internalProblemsService)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
            _mapper = mapper;
            _internalProblemsService = internalProblemsService;
        }

        public async Task<long> AddAsync(long userId, ProblemCollection collection)
        {
            // TODO: Add permission by userId?
            var dbEntry = _mapper.Map<Collection>(collection);
            await _repositoryUnitOfWork.GetRepository<ICollectionsRepository>().AddAsync(dbEntry);
            await _repositoryUnitOfWork.SaveChangesAsync();
            return dbEntry.Id;
        }

        public async Task<ProblemCollection> FindAsync(long id)
        {
            var dbEntry = await _repositoryUnitOfWork.GetRepository<ICollectionsRepository>().FindAsync(id);
            var problemCollection = _mapper.Map<ProblemCollection>(dbEntry);
            problemCollection.Problems = problemCollection.Problems
                .Select(_internalProblemsService.FetchProblemInfo)
                .Where(x => x != null)
                .ToArray();
            return problemCollection;
        }

        public async Task<ProblemCollection[]> GetRangeAsync(int offset, int limit)
        {
            var dbEntries = await _repositoryUnitOfWork.GetRepository<ICollectionsRepository>().GetRangeAsync(offset, limit);
            var mapped = dbEntries.Select(e => _mapper.Map<ProblemCollection>(e));
            return mapped.ToArray();
        }
    }
}