using System;
using System.Collections.Generic;

namespace DeliveryService.Domain.Services.Routing.Contracts {
   public struct Edge<T> : IEquatable<Edge<T>> where T : IEquatable<T> {
      public Edge(INode<T> node, int cost) {
         Node = node;
         Cost = cost;
      }

      public INode<T> Node { get; }
      public int Cost { get; }

      public bool Equals(Edge<T> other) => Node.Key == other.Node.Key && Cost == other.Cost &&
                                                  (EqualityComparer<T>.Default.Equals(Node.Item, default(T)) &&
          EqualityComparer<T>.Default.Equals(other.Node.Item, default(T)) ||
          !EqualityComparer<T>.Default.Equals(Node.Item, default(T)) && !EqualityComparer<T>.Default.Equals(other.Node.Item, default(T))
          && Node.Item.Equals(other.Node.Item));

      public override int GetHashCode() {
         int hash = 13;
         hash = hash * 7 + (int)Cost;
         hash = hash * 7 + (int)Node.Key;
         return hash;
      }

      public override bool Equals(object obj) {
         var other = obj as Edge<T>?;

         if (other == null)
            return false;

         return Equals(other.Value);
      }

   }
}
