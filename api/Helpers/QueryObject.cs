using System.ComponentModel.DataAnnotations;
using static api.Constants.ValidationHelper;

namespace api.Helpers;

public class QueryObject
{
    public string? Symbol { get; set; } = null;
    public string? CompanyName { get; set; } = null;
    [Range(1, int.MaxValue, ErrorMessage = NumberOutOfRange)]
    public int Page { get; set; } = 1;
    [Range(1, 20, ErrorMessage = NumberOutOfRange)]
    public int PageSize { get; set; } = 10;

    public string? SortBy { get; set; } = null;
    public bool IsSortAscending { get; set; } = true;
}