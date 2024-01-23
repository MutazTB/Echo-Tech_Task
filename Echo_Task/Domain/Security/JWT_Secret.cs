using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Security
{
    public class JWT_Secret
    {
        public static string Key { get; set; }
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
    }
}
