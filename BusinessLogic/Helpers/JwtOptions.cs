using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class JwtOptions
    {
        public string Key { get; set; } = string.Empty;
        public int Lifetime { get; set; } 
        public string Issuer { get; set; } = string.Empty;
    }
}
