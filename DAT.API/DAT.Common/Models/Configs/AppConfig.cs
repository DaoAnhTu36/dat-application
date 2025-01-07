namespace DAT.Common.Models.Configs
{
    public class AppConfig
    {
        public string? ServiceName { get; set; }
        public string? ServiceVersion { get; set; }
        public int ServicePort { get; set; }
        public string? ServiceBasePath { get; set; }
        public ApplicationSetting? ApplicationSetting { get; set; }
        public ConnectionStringInfo? ConnectionStringInfo { get; set; }
        public LoggingSetting? Logging { get; set; }
        public PreferenceSetting? Preference { get; set; }
        public FolderSetting? FolderSetting { get; set; }
        public PaginationSetting? PaginationSetting { get; set; }
        public GoogleSetting? GoogleSettings { get; set; }
    }
}