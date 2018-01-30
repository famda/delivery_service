using DeliveryService.Domain.Model;
using System;
using System.Collections.Generic;

namespace DeliveryService.Infrastructure.Initializers {
   internal static class Seed {
      public static IEnumerable<Route> GetTestRoutes() {
         var a = new Place(Guid.NewGuid(), "A");
         var b = new Place(Guid.NewGuid(), "B");
         var c = new Place(Guid.NewGuid(), "C");
         var d = new Place(Guid.NewGuid(), "D");
         var e = new Place(Guid.NewGuid(), "E");
         var f = new Place(Guid.NewGuid(), "F");
         var g = new Place(Guid.NewGuid(), "G");
         var h = new Place(Guid.NewGuid(), "H");
         var i = new Place(Guid.NewGuid(), "I");

         return new List<Route>() {
            new Route(Guid.NewGuid(), a, e, 30, 5),
            new Route(Guid.NewGuid(), a, c, 1, 20),
            new Route(Guid.NewGuid(), a, h, 10, 1),
            new Route(Guid.NewGuid(), b, c, 1, 12),
            new Route(Guid.NewGuid(), b, i, 65, 5),
            new Route(Guid.NewGuid(), b, g, 64, 73),
            new Route(Guid.NewGuid(), c, a, 1, 20),
            new Route(Guid.NewGuid(), c, b, 1, 12),
            new Route(Guid.NewGuid(), d, e, 3, 5),
            new Route(Guid.NewGuid(), d, f, 4, 50),
            new Route(Guid.NewGuid(), e, a, 30, 5),
            new Route(Guid.NewGuid(), e, h, 30, 1),
            new Route(Guid.NewGuid(), e, d, 3, 5),
            new Route(Guid.NewGuid(), f, d, 4, 50),
            new Route(Guid.NewGuid(), f, g, 40, 50),
            new Route(Guid.NewGuid(), f, i, 45, 50),
            new Route(Guid.NewGuid(), g, f, 40, 50),
            new Route(Guid.NewGuid(), g, b, 64, 73),
            new Route(Guid.NewGuid(), h, a, 10, 1),
            new Route(Guid.NewGuid(), h, e, 30, 1),
            new Route(Guid.NewGuid(), i, f, 45, 50),
            new Route(Guid.NewGuid(), i, b, 65, 5),
         };
      }
   }
}
