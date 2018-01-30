using DeliveryService.Domain.Services.Routing.Contracts;
using System;
using System.Collections.Generic;

namespace DeliveryService.Domain.Services.Routing {
   public class Node<T> : INode<T> where T : IEquatable<T> {
      public Node(uint key, T item) {
         Key = key;
         Item = item;
         Distance = Int32.MaxValue;
      }

      public IList<Edge<T>> Children { get; } = new List<Edge<T>>();
      public uint Key { get; }
      public T Item { get; }
      public int Distance { get; set; }
   }
}
