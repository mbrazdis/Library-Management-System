using System;
using System.Collections.Generic;

namespace LibraryMB3.Models;

public partial class Book
{
    public int BookId { get; set; }

    public int BookTypeId { get; set; }

    public int PublisherId { get; set; }

    public string Title { get; set; } = null!;

    public int PublishYear { get; set; }

    public int Stock { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();

    public virtual BookType BookType { get; set; } = null!;

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
}
