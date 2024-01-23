using Domain.Entiteis;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ComplaintDto : BaseEntity
    {
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public int Status { get; set; }

        public string UserIdentityFileName { get; set; }

        public IFormFile UserIdentity { get; set; }

        public List<string> Demands { get; set; }
    }
}
