using DAT.Common.Models.Configs;
using Microsoft.Extensions.Options;

namespace DAT.Common.Utilities
{
    public static class UtilityDatabase
    {
        public static IEnumerable<T> PaginationExtension<T>(IOptions<AppConfig> options, IQueryable<T> data, int pageNumber, int pageSize, out int totalPage, out int currentPage)
        {
            var totalItems = data.Count();
            currentPage = pageNumber == 0 ? 1 : pageNumber;
            pageSize = options.Value.PaginationSetting?.PageSize ?? 10;
            var skipNumber = (pageNumber - 1) * pageSize;
            totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
            return data.ToList().Skip(skipNumber).Take(pageSize);
        }
    }
}