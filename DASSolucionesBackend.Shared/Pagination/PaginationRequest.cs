namespace DASSolucionesBackend.Shared.Pagination;

public sealed record PaginationRequest(
    int PageIndex = 1,
    int PageSize = 10,
    string Order = "asc",
    string Sort = "Id",
    string? Search = null,
    int? NumberFilter = null,
    int? StateFilter = null);