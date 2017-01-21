namespace StudentProgress.Authorization.Entities.Identity
{
    public class IdentityUserLogin
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public long UserId { get; set; }
    }
}
