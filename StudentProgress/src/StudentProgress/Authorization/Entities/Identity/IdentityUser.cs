using System;
using System.Collections.Generic;

namespace StudentProgress.Authorization.Entities.Identity
{
    public class IdentityUser
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<IdentityUserRole> Roles { get; } = new List<IdentityUserRole>();
        public virtual ICollection<IdentityUserClaim> Claims { get; } = new List<IdentityUserClaim>();
        public virtual ICollection<IdentityUserLogin> Logins { get; } = new List<IdentityUserLogin>();

        public override string ToString()
        {
            return UserName;
        }
    }
}
