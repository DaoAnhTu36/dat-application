namespace DAT.Common.Models.Configs
{
    public class ApplicationMode
    {
        public static bool IsDevelopment => EnvironmentName != "Production" && EnvironmentName != "Staging";

        public static bool IsLocal => EnvironmentName == "Local";

        public static string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
    }
}