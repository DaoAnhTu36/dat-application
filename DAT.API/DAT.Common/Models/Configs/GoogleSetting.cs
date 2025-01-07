namespace DAT.Common.Models.Configs
{
    public class GoogleSetting
    {
        public OAuth2? OAuth2 { get; set; }
    }

    public class OAuth2
    {
        public string? ClientId { get; set; }
        public string? ProjectId { get; set; }
        public string? AuthUri { get; set; }
        public string? TokenUri { get; set; }
        public string? AuthProviderX509CertUrl { get; set; }
        public string? ClientSecret { get; set; }
    }
}