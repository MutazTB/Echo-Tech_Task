using Domain.Entiteis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IDemandSvc : IBaseSvc<Demand>
    {
        Task<List<Demand>> GetAllByComplaintId(Guid Id);
    }
}
