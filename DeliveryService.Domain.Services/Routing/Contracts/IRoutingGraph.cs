using System;

namespace DeliveryService.Domain.Services.Routing.Contracts {
   public interface IRoutingGraph<T> where T : IEquatable<T> {
      INode<T> this[uint node] { get; }
      uint FindIndex(T item);
   }
}
