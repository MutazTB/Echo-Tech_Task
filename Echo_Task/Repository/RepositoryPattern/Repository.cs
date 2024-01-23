using Domain.DTOs;
using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepositoryPattern
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly EchoTaskDBContext _EchoTaskDBContext;
        private DbSet<T> entities;

        public Repository(EchoTaskDBContext applicationDbContext)
        {
            _EchoTaskDBContext = applicationDbContext;
            entities = _EchoTaskDBContext.Set<T>();
        }

        public async Task<int> Delete(T entity)
        {
            int result = 0;
            if (entity == null)
            {
                return result;
            }
            entities.Remove(entity);
            result = await _EchoTaskDBContext.SaveChangesAsync();
            return result;
        }

        public async Task<T> Get(Guid Id)
        {
            return await entities.SingleOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return entities.AsEnumerable();
        }

        public async Task<int> Insert(T entity)
        {
            int result = 0;
            if (entity == null)
            {
                return result;
            }
            entities.Add(entity);
            result = await _EchoTaskDBContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Update(T entity)
        {
            int result = 0;
            if (entity == null)
            {
                return result;
            }
            entities.Update(entity);
            result = await _EchoTaskDBContext.SaveChangesAsync();
            return result;
        }
    }
}
