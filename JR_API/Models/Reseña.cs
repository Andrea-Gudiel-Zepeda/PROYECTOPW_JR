using System;
using System.Collections.Generic;

namespace JR_API.Models;

public partial class Reseña
{
    public int IdReseña { get; set; }

    public string NameBook { get; set; } = null!;

    public string AuthorBook { get; set; } = null!;

    public string GeneroBook { get; set; } = null!;

    public int PagesBook { get; set; }

    public int PuntuacionBook { get; set; }

    public int PuntuacionTrama { get; set; }

    public int PuntuacionPersonajes { get; set; }

    public string DescriptionReseña { get; set; } = null!;

    public string FavoritePhrase { get; set; } = null!;

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
