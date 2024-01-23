using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class DemandDto
    {
        public Guid Id { get; set; }

        [Required]
        public string ComplaintDemand { get; set; }

        [Required]
        public Guid Complaint_Id { get; set; }
    }
}
