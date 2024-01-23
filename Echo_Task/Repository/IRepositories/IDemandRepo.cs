using Domain.Entiteis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IDemandRepo : IBaseRepo<Demand>
    {
        Task<List<Demand>> GetAllByComplaintId(Guid Id);
    }
}
