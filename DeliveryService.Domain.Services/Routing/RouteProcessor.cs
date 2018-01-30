using DeliveryService.Domain.Services.Routing.Contracts;
using DeliveryService.Domain.Services.Routing.Extensions;
using System;
using System.Collections.Generic;

namespace DeliveryService.Domain.Services.Routing {
   public class RouteProcessor<T> where T : IEquatable<T> {
      protected readonly IRoutingGraph<T> _graph;

      public RouteProcessor(IRoutingGraph<T> graph) { _graph = graph; }

      public virtual IPathResult Process(T origin, T destination) {

         uint @from = _graph.FindIndex(origin);
         uint to = _graph.FindIndex(destination);

         var result = new RoutePathResult(from, to);
         _graph[from].Distance = 0;
         var q = new SortedSet<INode<T>>(new[] { _graph[from] }, new NodeComparer<T>());
         var current = new HashSet<uint>();

         while (q.Count > 0) {
            INode<T> u = q.Deque();
            current.Remove(u.Key);

            if (u.Key == to) {
               result.Distance = u.Distance;
               break;
            }

            for (int i = 0; i < u.Children.Count; i++) {
               Edge<T> e = u.Children[i];

               if (e.Node.Distance > u.Distance + e.Cost) {
                  if (current.Contains(e.Node.Key))
                     q.Remove(e.Node);

                  e.Node.Distance = u.Distance + e.Cost;
                  q.Add(e.Node);
                  current.Add(e.Node.Key);
                  result.Path[e.Node.Key] = u.Key;
               }
            }
         }
         return result;
      }
   }
}
