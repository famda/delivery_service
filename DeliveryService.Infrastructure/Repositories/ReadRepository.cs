using DeliveryService.Domain.Core.Model;
using DeliveryService.Domain.Core.Repositories;
using DeliveryService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeliveryService.Infrastructure.Repositories {
   internal class ReadRepository<D> : IReadRepository<D>, IDisposable where D : class, IEntityBase {

      internal readonly DeliveryServiceDbContext _context;
      public ReadRepository(DeliveryServiceDbContext context) { _context = context; }

      public virtual IEnumerable<D> GetAll() => _context.Set<D>();
      public virtual async Task<IEnumerable<D>> GetAllAsync() => await _context.Set<D>().ToListAsync();
      public IEnumerable<D> GetAllIncluding(params Expression<Func<D, object>>[] includeProperties) {
         IQueryable<D> queryable = GetAll().AsQueryable();
         foreach (Expression<Func<D, object>> includeProperty in includeProperties)
            queryable = queryable.Include<D, object>(includeProperty);
         return queryable;
      }
      public virtual IEnumerable<D> Get(Func<D, bool> predicate) => GetAll().Where(predicate);
      public virtual async Task<IEnumerable<D>> GetAsync(Expression<Func<D, bool>> predicate) => await _context.Set<D>().Where(predicate).ToListAsync();
      public void Dispose() => _context.Dispose();
   }
}
