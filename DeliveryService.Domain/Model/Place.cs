using DeliveryService.Domain.Core.Model;
using System;

namespace DeliveryService.Domain.Model {
   public class Place : EntityBase<Guid>, IEquatable<Place> {
      public string Description { get; private set; }
      protected Place() { }

      public Place(Guid id, string description) : base(id) {
         Description = string.IsNullOrEmpty(description) ? throw new ArgumentException("The description is required.") : description;
      }

      public bool Equals(Place other) {
         return base.Equals(other);
      }
   }
}
