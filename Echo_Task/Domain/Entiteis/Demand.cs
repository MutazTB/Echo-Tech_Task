﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;

namespace Domain.Entiteis
{
    public class Demand : BaseEntity
    {
        [Required]
        public string ComplaintDemand { get; set; }

        [ForeignKey("Complaint")]
        public Guid Complaint_Id { get; set; }

        public Complaint Complaint { get; set; }
    }
}
