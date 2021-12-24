using Microsoft.EntityFrameworkCore;
using PlaneSpotters.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotters.DataAccess.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class,IBaseEntity
    {
        protected DbSet<T> _context { get; set; }

        public BaseRepository(DbSet<T> context)
        {
            this._context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
            return (await this._context.AddAsync(entity)).Entity;
        }

        public Task CreateRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity != null)
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity).ConfigureAwait(true);
            }
        }

        public Task DeleteListAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindAsync(object keys)
        {
            var entity = await _context.FindAsync(keys);
            return (entity != null && !entity.IsDeleted) ? entity : null;
        }

        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await GetAll().Where(expression).FirstOrDefaultAsync().ConfigureAwait(true);
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            var entity = await GetAll().Where(x => x.InternalId.Equals(id)).FirstOrDefaultAsync().ConfigureAwait(true);
            return (entity != null && !entity.IsDeleted) ? entity : null;
        }

        public IQueryable<T> FindQueryById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return GetAll().Where(expression);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return (await Task.Run(() => _context.Update(entity)).ConfigureAwait(true)).Entity;
        }

        public Task UpdateListAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
