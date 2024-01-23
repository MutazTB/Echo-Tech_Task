using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ReturnResult
    {
        public string Status { get; set; }

        public string Code { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }
    }
}
