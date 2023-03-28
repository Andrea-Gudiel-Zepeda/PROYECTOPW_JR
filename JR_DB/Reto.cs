using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JR_DB
{
    public class Reto
    {
        public int IdReto { get; set; }

        public string NombreReto { get; set; } = null!;

        public string Status { get; set; } = null!;

        public int IdUser { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }
    }
}
