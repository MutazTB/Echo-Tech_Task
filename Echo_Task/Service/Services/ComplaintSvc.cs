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
    public class ComplaintSvc : IComplaintSvc
    {
        private IComplaintRepo _complaintRepo;
        public ComplaintSvc(IComplaintRepo complaintRepo)
        {
            _complaintRepo = complaintRepo;
        }

        public Task<int> Delete(Complaint complaint)
        {
            return _complaintRepo.Delete(complaint);
        }

        public Task<Complaint> Get(Guid Id)
        {
            return _complaintRepo.Get(Id);
        }

        public Task<IEnumerable<Complaint>> GetAll()
        {
            return _complaintRepo.GetAll();
        }

        public Task<int> Insert(Complaint complaint)
        {
            return _complaintRepo.Insert(complaint);
        }

        public Task<int> Update(Complaint complaint)
        {
            return _complaintRepo.Update(complaint);
        }
    }
}
