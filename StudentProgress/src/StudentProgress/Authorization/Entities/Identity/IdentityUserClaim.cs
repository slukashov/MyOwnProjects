namespace StudentProgress.Authorization.Entities.Identity
{
    public class IdentityUserClaim
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
