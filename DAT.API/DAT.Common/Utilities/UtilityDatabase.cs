using DAT.Common.Models.Configs;
using Microsoft.Extensions.Options;

namespace DAT.Common.Utilities
{
    public static class UtilityDatabase
    {
        public static IEnumerable<T> PaginationExtension<T>(IOptions<AppConfig> options, IQueryable<T> data, int pageNumber = 0, int pageSize = 0)
        {
            pageSize = options.Value.PaginationSetting?.PageSize ?? 10;
            var skipNumber = (pageNumber - 1) * pageSize;
            return data.ToList().Skip(skipNumber).Take(pageSize);
        }
    }
}