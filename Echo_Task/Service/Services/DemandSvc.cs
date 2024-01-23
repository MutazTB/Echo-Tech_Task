using Domain.Entiteis;
using Repositories.IRepositories;
using Repositories.RepositoryPattern;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DemandSvc : IDemandSvc
    {
        private IDemandRepo _demandRepo;
        public DemandSvc(IDemandRepo demandRepo)
        {
            _demandRepo = demandRepo;
        }

        public Task<int> Delete(Demand demand)
        {
           return _demandRepo.Delete(demand);
        }

        public Task<Demand> Get(Guid Id)
        {
            return _demandRepo.Get(Id);
        }

        public Task<IEnumerable<Demand>> GetAll()
        {
            return _demandRepo.GetAll();
        }

        public Task<List<Demand>> GetAllByComplaintId(Guid Id)
        {
            return _demandRepo.GetAllByComplaintId(Id);
        }

        public Task<int> Insert(Demand demand)
        {
            return _demandRepo.Insert(demand);
        }

        public Task<int> Update(Demand demand)
        {
            return _demandRepo.Update(demand);
        }
    }
}
