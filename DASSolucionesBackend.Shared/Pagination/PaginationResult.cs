namespace DASSolucionesBackend.Shared.Pagination;

public class PaginationResult<T>(int pageIndex, int pageSize, long count, List<T> data) where T : class
{
    public int PageIndex => pageIndex;
    public int PageSize => pageSize;
    public long Count => count;
    public List<T> Data => data;
}