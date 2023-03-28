using System;
using System.Collections.Generic;

namespace JR_API.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string FullName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int NumberPhone { get; set; }

    public string Password { get; set; } = null!;

    public virtual Book? Book { get; set; }

    public virtual ICollection<Calificacion> Calificacions { get; } = new List<Calificacion>();

    public virtual ICollection<Reseña> Reseñas { get; } = new List<Reseña>();
}
