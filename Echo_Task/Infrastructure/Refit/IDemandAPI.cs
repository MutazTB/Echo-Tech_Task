using Domain.DTOs;
using Domain.Entiteis;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Refit
{
    public interface IDemandAPI
    {
        [Get("/GetAllDemands")]
        Task<ReturnResult> GetAllDemand();

        [Get("/GetDemand/{Id}")]
        Task<ReturnResult> GatDemand(Guid Id);

        [Post("/AddDemand")]
        Task<ReturnResult> Add([Body] DemandDto request);

        [Post("/UpdateDemand")]
        Task<ReturnResult> Update(Guid Id, [Body] DemandDto request);

        [Post("/DeleteDemand")]
        Task<ReturnResult> Delete(Guid Id);
    }
}
