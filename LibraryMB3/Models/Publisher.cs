using System;
using System.Collections.Generic;

namespace LibraryMB3.Models;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string Name { get; set; } = null!;

    public string? Adress { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
