namespace StudentProgress.Authorization.Entities.Identity
{
    public class IdentityRoleClaim
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
