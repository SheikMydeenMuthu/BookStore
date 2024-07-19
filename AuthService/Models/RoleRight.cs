using System;
using System.Collections.Generic;

namespace AuthService.Models
{
    public partial class RoleRight
    {
        public RoleRight()
        {
            Users = new HashSet<User>();
        }

        public int RoleRightId { get; set; }
        public int RoleId { get; set; }
        public string? ScreenName { get; set; }
        public string? Rights { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
    }
}
