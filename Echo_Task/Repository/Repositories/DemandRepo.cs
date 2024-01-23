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
    public class DemandRepo : IDemandRepo
    {
        private readonly EchoTaskDBContext _Context;
        public DemandRepo(EchoTaskDBContext context)
        {
            _Context = context;
        }
        public async Task<int> Insert(Demand demand)
        {
            int result = 0;
            try
            {
                if (demand == null)
                {
                    return result;
                }
                _Context.Entry(demand).State = EntityState.Added;
                result = await _Context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public async Task<int> Delete(Demand demand)
        {
            int result = 0;
            try
            {
                if (demand == null)
                {
                    return result;
                }
                _Context.Entry(demand).State = EntityState.Modified;
                result = await _Context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public async Task<IEnumerable<Demand>> GetAll()
        {
            return await _Context.Demands.ToListAsync();
        }

        public async Task<Demand> Get(Guid Id)
        {
            return await _Context.Demands.Where(C => C.Id == Id && C.IsDeleted != true).SingleOrDefaultAsync();
        }

        public async Task<List<Demand>> GetAllByComplaintId(Guid ComplaintId)
        {
            return await _Context.Demands.Where(C => C.Complaint_Id == ComplaintId && C.IsDeleted != true).ToListAsync();
        }

        public async Task<int> Update(Demand demand)
        {
            int result = 0;
            try
            {
                if (demand == null)
                {
                    return result;
                }
                _Context.Entry(demand).State = EntityState.Modified;
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
