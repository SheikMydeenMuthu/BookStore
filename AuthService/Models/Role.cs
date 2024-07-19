using System;
using System.Collections.Generic;

namespace AuthService.Models
{
    public partial class Role
    {
        public Role()
        {
            RoleRights = new HashSet<RoleRight>();
        }

        public int RoleId { get; set; }
        public string? UserRole { get; set; }

        public virtual ICollection<RoleRight> RoleRights { get; set; }
    }
}
