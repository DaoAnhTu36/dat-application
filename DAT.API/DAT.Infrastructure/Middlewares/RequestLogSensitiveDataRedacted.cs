namespace DAT.Infrastructure.Middlewares
{
    public class SensitiveDataRedacted
    {
        private static readonly string[] SensitiveHeaders =
        {
            "AUTHORIZATION",
            "COOKIE",
            "SET-COOKIE",
        };

        private static string _redactedString = "****REDACTED****";

        public static string HeaderRedactedString(string key, string value)
        {
            return SensitiveHeaders.Contains(key.Trim().ToUpper())
                ? _redactedString
                : value;
        }
    }
}