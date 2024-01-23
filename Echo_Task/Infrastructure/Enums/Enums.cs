using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Enums
{
    public class Enums
    {
        public enum ResultStatus
        {
            Success = 1,
            Error = 2,
            NotAuthorize = 3
        }
        public enum ComplaintStatus
        {
            Approved = 1,
            Rejected = 2,
            Pending = 3
        }
    }
}
