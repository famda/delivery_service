using DeliveryService.Domain.Services.Routing.Contracts;
using System;
using System.Collections.Generic;

namespace DeliveryService.Domain.Services.Routing.Extensions {
   internal class NodeComparer<T> : IComparer<INode<T>> where T : IEquatable<T> {
      public int Compare(INode<T> x, INode<T> y) {
         int comparer = x.Distance.CompareTo(y.Distance);

         if (comparer == 0)
            return x.Key.CompareTo(y.Key);

         return comparer;
      }
   }
}
