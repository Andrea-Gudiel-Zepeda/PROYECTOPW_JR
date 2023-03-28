using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JR_DB
{
    public class Calificacion
    {
        public int IdCalificacion { get; set; }

        public int LimiteInferior { get; set; }

        public int LimiteSuperior { get; set; }

        public int IdUser { get; set; }

    }
}
