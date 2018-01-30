using DeliveryService.Domain.Core.Model;
using System;

namespace DeliveryService.Domain.Model {
   public class Route : EntityBase<Guid> {

      public int Time { get; private set; }
      public int Cost { get; private set; }
      public Place Origin { get; private set; }
      public Guid OriginId { get; private set; }
      public Guid DestinationId { get; private set; }
      public Place Destination { get; private set; }

      protected Route() { }

      public Route(Guid id, Place origin, Place destination, int time, int cost) : base(id) {
         Origin = origin ?? throw new ArgumentException("The origin place is required.");
         OriginId = origin.Id;
         Destination = destination ?? throw new ArgumentException("The destination place is required.");
         DestinationId = destination.Id;
         Time = time < 0 ? throw new ArgumentException("The time cost needs to be positive.") : time;
         Cost = cost < 0 ? throw new ArgumentException("The cost needs to be positive.") : cost;
      }
      public override string ToString() => $"{Origin.Description}->{Destination.Description}";
   }
}
