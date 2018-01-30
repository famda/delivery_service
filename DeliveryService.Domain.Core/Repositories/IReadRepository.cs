using DeliveryService.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeliveryService.Domain.Core.Repositories {
   public interface IReadRepository<T> where T : class, IEntityBase {
      IEnumerable<T> GetAll();
      IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
      IEnumerable<T> Get(Func<T, bool> predicate);
      Task<IEnumerable<T>> GetAllAsync();
      Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
   }
}
