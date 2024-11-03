namespace DASSolucionesBackend.Shared.Pagination;

public sealed record PaginationRequest(int PageIndex = 0, int PageSize = 10, string? Search = null);