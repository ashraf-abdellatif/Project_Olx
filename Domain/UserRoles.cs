using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UserRoles:IdentityUserRole<string>
    {
        public Role Role { get; set; }
        public AppUser AppUser { get; set; }
    }
}
