namespace HR.LeaveManagement.Application.Models.Identity
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string EncrypKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
