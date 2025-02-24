﻿using Domain.Common;
using Domain.Entities.Items;
using Domain.Entities.Members;
using Domain.Entities.Ratings;

namespace Domain.Repositories;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(ItemId id, CancellationToken cancellationToken = default);
    Task<PagedResult<Item>> GetAsync(IEnumerable<string>? keywords, string? searchTErm, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedResult<Rating>> GetRatingsAsync(ItemId id, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken = default);
    Task Rate(ItemId itemId, MemberId memberId, int rate, string comment, CancellationToken cancellationToken = default);
    Task AddAsync(Item item, CancellationToken cancellationToken = default);
    void Update(Item item); 
    void Remove(Item item);
}
