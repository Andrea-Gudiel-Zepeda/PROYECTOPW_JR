using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JR_DB
{
    public class User
    {
        public int IdUser { get; set; }

        public string FullName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int NumberPhone { get; set; }

        public string Password { get; set; } = null!;

    }
}
