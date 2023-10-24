using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.CatalogService.Abstractions.DB;

namespace Module_02.Task_02.CatalogService.DAL.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query,
        int page, int pageSize) where T : class
    {
        var result = new PagedResult<T>
        {
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = await query.CountAsync()
        };


        var pageCount = (double)result.RowCount / pageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;
        result.Records = await query.Skip(skip).Take(pageSize).ToArrayAsync();

        return result;
    }
}