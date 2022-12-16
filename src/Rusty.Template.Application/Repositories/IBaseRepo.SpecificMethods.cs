using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Domain;

namespace Rusty.Template.Application.Repositories;

/// <summary>
///     The base repo interface
/// </summary>
public partial interface IBaseRepo<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    ///     Paginates the request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="includes">The includes</param>
    /// <returns>A task containing a paged response of t entity</returns>
    Task<PagedResponse<TEntity>> PaginateAsync(OrderByPagedRequest request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    /// <summary>
    ///     Paginates the request
    /// </summary>
    /// <typeparam name="TResult">The result</typeparam>
    /// <param name="request">The request</param>
    /// <param name="includes">The includes</param>
    /// <returns>A task containing a paged response of t result</returns>
    Task<PagedResponse<TResult>> PaginateAsync<TResult>(OrderByPagedRequest request,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);
}