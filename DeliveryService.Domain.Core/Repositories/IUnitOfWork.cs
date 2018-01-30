using DeliveryService.Domain.Core.Model;

namespace DeliveryService.Domain.Core.Repositories {
   public interface IUnitOfWork {
      IWriteRepository<T> WriteRepository<T>() where T : class, IEntityBase;
      void Commit();
      void RollBack();
   }
}
