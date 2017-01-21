using System;
using System.Collections.Generic;

namespace StudentProgress.Authorization.Entities.Identity
{
    public class IdentityRole
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        public virtual ICollection<IdentityUserRole> Users { get; } = new List<IdentityUserRole>();
        public virtual ICollection<IdentityRoleClaim> Claims { get; } = new List<IdentityRoleClaim>();

        public override string ToString()
        {
            return Name;
        }
    }
}
