using System;
using System.Collections.Generic;

namespace JR_MVC.Models;

public partial class CategorieBook
{
    public int IdCategorie { get; set; }

    public string NameCategorie { get; set; } = null!;

    public virtual Book? Book { get; set; }
}
