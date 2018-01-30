using DeliveryService.Domain.Services.Routing.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryService.Domain.Services.Routing {
   public class RoutingGraph<T> : IRoutingGraph<T>, IEnumerable<INode<T>> where T : IEquatable<T> {

      private readonly IDictionary<uint, INode<T>> _nodes = new Dictionary<uint, INode<T>>();

      public void AddNode(T item) {
         uint key = (uint)_nodes.Count;
         AddNode(key, item);
      }

      protected void AddNode(uint key, T item) {
         if (_nodes.ContainsKey(key))
            throw new InvalidOperationException("Node have to be unique.", new Exception("The same key of node."));

         _nodes.Add(key, new Node<T>(key, item));
      }

      public uint FindIndex(T item) {
         var obj = _nodes.FirstOrDefault(x => x.Value.Item.Equals(item));
         return obj.Key;
      }

      public bool Connect(T origin, T destination, int cost) {
         uint from = FindIndex(origin);
         uint to = FindIndex(destination);
         INode<T> nodeFrom = this[from];
         INode<T> nodeTo = this[to];
         nodeFrom.Children.Add(new Edge<T>(nodeTo, cost));
         return true;
      }

      public IEnumerator<INode<T>> GetEnumerator() => _nodes.Select(x => x.Value).GetEnumerator();
      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

      public INode<T> this[uint node] => _nodes[node];
   }
}
