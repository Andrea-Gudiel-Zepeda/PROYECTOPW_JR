using System;
using System.Collections.Generic;

namespace JR_API.Models;

public partial class CategorieBook
{
    public int IdCategorie { get; set; }

    public string NameCategorie { get; set; } = null!;

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
