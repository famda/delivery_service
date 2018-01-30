using System;
using System.Collections.Generic;

namespace DeliveryService.Domain.Services.Routing.Contracts {
   public interface INode<T> where T : IEquatable<T> {
      IList<Edge<T>> Children { get; }
      T Item { get; }
      uint Key { get; }
      int Distance { get; set; }
   }
}
