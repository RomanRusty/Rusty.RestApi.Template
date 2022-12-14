using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.Requests;
using Rusty.Template.Contracts.Responses;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Infrastructure.Repositories.Extensions;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepo;

/// <summary>
///     The base repo class
/// </summary>
public partial class BaseGenericRepo<TContext, TEntity> where TEntity : class where TContext : DbContext
{
    /// <summary>
    ///     Paginates the skip items
    /// </summary>
    /// <param name="skipItems">The skip items</param>
    /// <param name="takeItems">The take items</param>
    /// <param name="orderBy">The order by</param>
    /// <param name="orderDirection">The order direction</param>
    /// <param name="expression">The expression</param>
    /// <returns>A queryable of t entity collection and int total count</returns>
    public virtual ( IQueryable<TEntity> Collection, int TotalCount) Paginate(int skipItems, int takeItems,
                                                                              string orderBy,
                                                                              OrderDirection orderDirection,
                                                                              Expression<Func<TEntity, bool>>?
                                                                                  expression)
    {
        var query = DbSet.WhereNullable(expression);
        
        query = query.OrderByWithDirection(orderBy, orderDirection);

        return query.PaginateWithTotalCount(skipItems, takeItems);
    }

    /// <summary>
    ///     Paginates the request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken"></param>
    /// <param name="includes">The includes</param>
    /// <returns>A task containing a paged response of t entity</returns>
    public async Task<PagedResponse<TEntity>> PaginateAsync(OrderedPagedRequest request,
        CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        var query = IncludeIfNotNull(includes);
        query = query.OrderByWithDirection(request.OrderByData);
        return await query.PaginateWithTotalCountAsListAsync(request.PageData, cancellationToken);
    }


    /// <summary>
    ///     Paginates the request
    /// </summary>
    /// <typeparam name="TResult">The result</typeparam>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken"></param>
    /// <param name="includes">The includes</param>
    /// <returns>A task containing a paged response of t result</returns>
    public async Task<PagedResponse<TResult>> PaginateAsync<TResult>(OrderedPagedRequest request,
        CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null) where TResult : class
    {
        var query = IncludeIfNotNull(includes);
        var result = query.ProjectToType<TResult>();
        result = result.OrderByWithDirection(request.OrderByData.OrderBy, request.OrderByData.OrderDirection);
        return await result.PaginateWithTotalCountAsListAsync(request.PageData, cancellationToken);
    }
}