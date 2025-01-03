using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Helpers;

public static class StockQueryExtensions
{
    public static IQueryable<Stock> ApplyFilters(this IQueryable<Stock> query, QueryObject queryObject)
    {
        if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            query = query.Where(s => s.Symbol.Contains(queryObject.Symbol));

        if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            query = query.Where(s => s.CompanyName.Contains(queryObject.CompanyName));

        return query;
    }

    public static IQueryable<Stock> ApplyPaging(this IQueryable<Stock> query, QueryObject queryObject, int totalCount)
    {
        var totalPages = (int)Math.Ceiling(totalCount / (double)queryObject.PageSize);

        if (queryObject.Page > totalPages)
            queryObject.Page = totalPages;

        return query.Skip((queryObject.Page - 1) * queryObject.PageSize)
                    .Take(queryObject.PageSize);
    }
}
