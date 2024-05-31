using System;
using System.Collections.Generic;

namespace LibraryMB3.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
}
