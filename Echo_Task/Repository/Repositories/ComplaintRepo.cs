using Domain.Entiteis;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ComplaintRepo : IComplaintRepo
    {
        private readonly EchoTaskDBContext _Context;
        public ComplaintRepo(EchoTaskDBContext context)
        {
            _Context = context;
        }
        public async Task<int> Insert(Complaint complaint)
        {
            int result = 0;
            try
            {
                if (complaint == null)
                {
                    return result;
                }
                _Context.Entry(complaint).State = EntityState.Added;
                result = await _Context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return result;
            }

        }

        public async Task<int> Delete(Complaint complaint)
        {
            int result = 0;
            try
            {
                if (complaint == null)
                {
                    return result;
                }
                _Context.Entry(complaint).State = EntityState.Modified;
                result = await _Context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public async Task<List<Complaint>> GetByStatus(int status)
        {
            return await _Context.Complaints.Where(C => C.Status == status && C.IsDeleted != true).ToListAsync();
        }

        public async Task<IEnumerable<Complaint>> GetAll()
        {
            return await _Context.Complaints.Where(c => c.IsDeleted != true).ToListAsync();
        }

        public async Task<Complaint> Get(Guid complaint)
        {
            return await _Context.Complaints.Where(C => C.Id == complaint && C.IsDeleted != true).SingleOrDefaultAsync();
        }

        public async Task<int> Update(Complaint complaint)
        {
            int result = 0;
            try
            {
                if (complaint == null)
                {
                    return result;
                }
                _Context.Entry(complaint).State = EntityState.Modified;
                result = await _Context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
    }
}
