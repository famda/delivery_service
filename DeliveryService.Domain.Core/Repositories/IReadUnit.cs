using DeliveryService.Domain.Core.Model;

namespace DeliveryService.Domain.Core.Repositories {
   public interface IReadUnit {
      IReadRepository<T> ReadRepository<T>() where T : class, IEntityBase;
   }
}
