using smartLaywer.NewFolder;
using System;
using System.Collections.Generic;

namespace smartLaywer.Models;

public partial class Role
{
    public int Id { get; set; }

    public UserRole RoleName { get; set; } = UserRole.Lawyer;


    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
