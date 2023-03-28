using System;
using System.Collections.Generic;

namespace JR_API.Models;

public partial class Book
{
    public int IdBook { get; set; }

    public string NameBook { get; set; } = null!;

    public string AuthorBook { get; set; } = null!;

    public DateTime BookPublish { get; set; }

    public DateTime? DateBook { get; set; }

    public int Calificacion { get; set; }

    public byte[] PictureBook { get; set; } = null!;

    public int IdCategorie { get; set; }

    public int IdUser { get; set; }

    public virtual CategorieBook IdCategorieNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
