namespace Module_02.Task_02.CatalogService.Abstractions.DB;

public abstract class PagedResultBase
{
    public virtual int CurrentPage { get; set; }
    public virtual int PageCount { get; set; }
    public virtual int PageSize { get; set; }
    public virtual int RowCount { get; set; }

    public virtual int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

    public virtual int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
}

public class PagedResult<T> : PagedResultBase where T : class
{
    public virtual T[] Records { get; set; }
}