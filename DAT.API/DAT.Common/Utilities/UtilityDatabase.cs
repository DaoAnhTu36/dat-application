using System.Text;
using System.Text.RegularExpressions;
using DAT.Common.Models.Configs;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

namespace DAT.Common.Utilities
{
    public static class UtilityDatabase
    {
        public static IEnumerable<T> PaginationExtension<T>(IOptions<AppConfig> options, IQueryable<T> data, int pageNumber, int pageSize, out int totalPage, out int currentPage)
        {
            var totalItems = data.Count();
            currentPage = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize > 0 ? pageSize : options.Value.PaginationSetting?.PageSize ?? 10;
            var skipNumber = (pageNumber - 1) * pageSize;
            totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
            return data.ToList().Skip(skipNumber).Take(pageSize);
        }

        public static IEnumerable<T> PaginationExtension<T>(IOptions<AppConfig> options, List<T> data, int pageNumber, int pageSize, out int totalPage, out int currentPage)
        {
            var totalItems = data.Count();
            currentPage = pageNumber == 0 ? 1 : pageNumber;
            pageSize = options.Value.PaginationSetting?.PageSize ?? 10;
            var skipNumber = (pageNumber - 1) * pageSize;
            totalPage = (int)Math.Ceiling((double)totalItems / pageSize);
            return data.ToList().Skip(skipNumber).Take(pageSize);
        }

        public static bool FindMatches(string? textSearch, string? dataSearch)
        {
            try
            {
                textSearch = ConvertTextToUnaccented(textSearch);
                dataSearch = ConvertTextToUnaccented(dataSearch);
                // Split the search text into individual words
                string[] words = textSearch.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Build a regex pattern to match all words (case-insensitive)
                string pattern = string.Join(".*", words.Select(Regex.Escape));

                // Filter the list of names using the regex
                return Regex.IsMatch(dataSearch, pattern, RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<string> FindMatches(string? textSearch, string[]? dataSearch)
        {
            try
            {
                textSearch = ConvertTextToUnaccented(textSearch);
                // Split the search text into individual words
                string[] words = textSearch.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Build a regex pattern to match all words (case-insensitive)
                string pattern = string.Join(".*", words.Select(Regex.Escape));

                // Filter the list of names using the regex
                return dataSearch
                    .Where(name => Regex.IsMatch(ConvertTextToUnaccented(name), pattern, RegexOptions.IgnoreCase))
                    .ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public static string ConvertTextToUnaccented(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}