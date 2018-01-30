using DeliveryService.Domain.Core.Model;
using DeliveryService.Domain.Core.Repositories;
using DeliveryService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DeliveryService.Infrastructure.Repositories {
   internal class WriteRepository<D> : IWriteRepository<D>, IDisposable where D : class, IEntityBase {
      internal readonly DeliveryServiceDbContext _context;
      public WriteRepository(DeliveryServiceDbContext context) { _context = context; }
      public virtual void Add(D obj) => _context.Set<D>().Add(obj);
      public void AddRange(IEnumerable<D> obj) => _context.Set<D>().AddRange(obj);

      public virtual void Update(D obj) {
         _context.Entry(obj).State = EntityState.Modified;
         _context.Entry(obj);
      }
      public virtual void Remove(D obj) {
         if (_context.Entry(obj).State == EntityState.Detached)
            _context.Set<D>().Attach(obj);
         _context.Set<D>().Remove(obj);
      }
      public void Dispose() { _context.Dispose(); }
   }
}
