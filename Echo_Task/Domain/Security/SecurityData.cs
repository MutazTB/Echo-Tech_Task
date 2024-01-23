using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Security
{
    public class SecurityData
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
        
        public string UserEmail { get; set; }

        public List<string> UserRoles { get; set; }

    }
}
