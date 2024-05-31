using System;
using System.Collections.Generic;

namespace LibraryMB3.Models;

public partial class BookType
{
    public int BookTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

}
