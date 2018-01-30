using DeliveryService.Domain.Model;
using System;
using Xunit;

namespace DeliveryService.Domain.Tests {
   public class DomainModelTests {

      [Fact]
      public void RouteEqualityOperatorTest() {
         var id = Guid.NewGuid();
         var origin = new Place(Guid.NewGuid(), "Lisbon");
         var destination = new Place(Guid.NewGuid(), "Porto");
         Route route1 = new Route(id, origin, destination, 10, 10);
         Route route2 = new Route(id, origin, destination, 10, 10);
         Route route3 = new Route(Guid.NewGuid(), origin, destination, 10, 10);

         Assert.True(route1 == route2);
         Assert.True(route1.Equals(route2));
         Assert.False(route1 == route3);
         Assert.False(route1.Equals(route3));
         Assert.False(route2 == route3);
         Assert.True(route2 != route3);
         Assert.True(route1 != route3);
      }

      [Fact]
      public void RouteConstructorTest() {
         var id = Guid.NewGuid();
         var origin = new Place(Guid.NewGuid(), "Lisbon");
         var destination = new Place(Guid.NewGuid(), "Porto");
         Route route = new Route(id, origin, destination, 10, 10);
         Assert.Equal(route.Id, id);
         Assert.Equal(route.Origin, origin);
         Assert.Equal(route.Destination, destination);
         Assert.Equal(route.DestinationId, destination.Id);
         Assert.Equal(route.OriginId, origin.Id);
         Assert.Equal(10, route.Time);
         Assert.Equal(10, route.Cost);
      }

      [Fact]
      public void RouteTimeShoulndBePositive() {
         var id = Guid.NewGuid();
         var origin = new Place(Guid.NewGuid(), "Lisbon");
         var destination = new Place(Guid.NewGuid(), "Porto");
         Assert.Throws<ArgumentException>(() => new Route(id, origin, destination, -10, 10));
      }

      [Fact]
      public void RouteCostShoulndBePositive() {
         var id = Guid.NewGuid();
         var origin = new Place(Guid.NewGuid(), "Lisbon");
         var destination = new Place(Guid.NewGuid(), "Porto");
         Assert.Throws<ArgumentException>(() => new Route(id, origin, destination, 10, -10));
      }

      [Fact]
      public void RouteOriginShouldntBeNull() {
         var id = Guid.NewGuid();
         var destination = new Place(Guid.NewGuid(), "Porto");
         Assert.Throws<ArgumentException>(() => new Route(id, null, destination, 10, -10));
      }

      [Fact]
      public void RouteDestinationShoudntBeNull() {
         var id = Guid.NewGuid();
         var origin = new Place(Guid.NewGuid(), "Lisbon");
         Assert.Throws<ArgumentException>(() => new Route(id, origin, null, 10, -10));
      }

      [Fact]
      public void PlaceConstructorTest() {
         var id = Guid.NewGuid();
         var place = new Place(id, "Lisbon");

         Assert.Equal("Lisbon", place.Description);
         Assert.Equal(id, place.Id);
      }

      [Fact]
      public void PlaceDescriptionShouldntBeEmpty() {
         var id = Guid.NewGuid();
         Assert.Throws<ArgumentException>(() => new Place(id, ""));
      }
   }
}
