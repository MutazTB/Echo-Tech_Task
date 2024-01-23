using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Refit
{
    public interface IComplaintAPI
    {
        [Get("/GetAllComplaints")]
        Task<ReturnResult> GetAllComplaints();

        [Get("/GetComplaint/{Id}")]
        Task<ReturnResult> GatComplaint(Guid Id);

        [Post("/AddComplaint")]
        Task<ReturnResult> Add(ComplaintDto request);

        [Post("/UpdateComplaint/{Id}")]
        Task<ReturnResult> Update(Guid Id, [Body] ComplaintDto request);

        [Post("/DeleteComplaint")]
        Task<ReturnResult> Delete(Guid Id);

        [Post("/RejectComplaint")]
        Task<ReturnResult> Reject(Guid Id);

        [Post("/ApproveComplaint")]
        Task<ReturnResult> Approve(Guid Id);
    }
}
