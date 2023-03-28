using System;
using System.Collections.Generic;

namespace JR_MVC.Models;

public partial class Calificacion
{
    public int IdCalificacion { get; set; }

    public int LimiteInferior { get; set; }

    public int LimiteSuperior { get; set; }

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
