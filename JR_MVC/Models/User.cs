using System;
using System.Collections.Generic;

namespace JR_MVC.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string FullName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int NumberPhone { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public virtual ICollection<Calificacion> Calificacions { get; } = new List<Calificacion>();

    public virtual ICollection<Reseña> Reseñas { get; } = new List<Reseña>();
}
