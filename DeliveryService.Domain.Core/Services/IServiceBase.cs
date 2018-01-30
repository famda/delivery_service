using DeliveryService.Domain.Core.Model;

namespace DeliveryService.Domain.Core.Services {
   public interface IServiceBase<T> where T : class, IEntityBase {
   }
}
