using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared
{
    public class LoginDto
    {
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBanned { get; set; }
    }
}
