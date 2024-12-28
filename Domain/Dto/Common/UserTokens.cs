namespace Domain.Dto.Common
{
    public class UserTokens
    {
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public TimeSpan Validaty { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public long Id { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
