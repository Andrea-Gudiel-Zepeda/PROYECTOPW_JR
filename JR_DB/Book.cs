using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JR_DB
{
    public class Book
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
    }
}
