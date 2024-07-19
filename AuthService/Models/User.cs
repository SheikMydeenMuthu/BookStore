using System;
using System.Collections.Generic;

namespace AuthService.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public int RoleRightId { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public virtual RoleRight RoleRight { get; set; } = null!;
    }
}
