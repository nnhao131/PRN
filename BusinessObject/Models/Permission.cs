using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
