using System;
using System.Collections.Generic;

namespace JR_MVC.Models;

public partial class Reto
{
    public int IdReto { get; set; }

    public string NombreReto { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int IdUser { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }
}
