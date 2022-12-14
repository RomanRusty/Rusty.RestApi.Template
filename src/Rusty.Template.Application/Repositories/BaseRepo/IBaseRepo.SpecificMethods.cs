using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Contracts.Responses;

namespace Rusty.Template.Application.Repositories.BaseRepo;

/// <summary>
///     The base repo interface
/// </summary>
public partial interface IBaseRepo<TEntity> where TEntity : class
{
    /// <summary>
    ///     Paginates the request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken"></param>
    /// <param name="includes">The includes</param>
    /// <returns>A task containing a paged response of t entity</returns>
    Task<PagedResponse<TEntity>> PaginateAsync(OrderedPagedRequest request, CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    /// <summary>
    ///     Paginates the request
    /// </summary>
    /// <typeparam name="TResult">The result</typeparam>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken"></param>
    /// <param name="includes">The includes</param>
    /// <returns>A task containing a paged response of t result</returns>
    Task<PagedResponse<TResult>> PaginateAsync<TResult>(OrderedPagedRequest request,
        CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null) where TResult : class;
}