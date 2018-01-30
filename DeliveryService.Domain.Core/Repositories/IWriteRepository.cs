using DeliveryService.Domain.Core.Model;
using System.Collections.Generic;

namespace DeliveryService.Domain.Core.Repositories {
   public interface IWriteRepository<T> where T : class, IEntityBase {
      void Add(T obj);
      void AddRange(IEnumerable<T> obj);
      void Update(T obj);
      void Remove(T obj);
   }
}
