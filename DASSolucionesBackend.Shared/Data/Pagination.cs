using DASSolucionesBackend.Shared.Pagination;
using System.Linq.Dynamic.Core;

namespace DASSolucionesBackend.Shared.Data;

public class Pagination
{
    public static string[] SplitStateFilter(string stateFilter)
    {
        const string delimiter = "-";
        return stateFilter.Split(delimiter);
    }

    public static IQueryable<TEntity> Ordering<TEntity>(PaginationRequest request, IQueryable<TEntity> queryable,
        bool pagination = false) where TEntity : class
    {
        string sortProperty = char.ToUpper(request.Sort[0]) + request.Sort.Substring(1);
        
        IQueryable<TEntity> query = request.Order == "desc"
            ? queryable.OrderBy($"{sortProperty} descending")
            : queryable.OrderBy($"{sortProperty} ascending");

        if (pagination) query = query.Paginate(request);

        return query;
    }
}

public static class QueryableHelper
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationRequest request)
    {
        return queryable.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
    }
}