using DeliveryService.Domain.Services.Extensions;

namespace DeliveryService.Domain.Services.Types {
   public class RouteCriteria : Enumeration {
      public RouteCriteria() { }
      private RouteCriteria(int value, string displayName) : base(value, displayName) { }

      public static readonly RouteCriteria Cost = new RouteCriteria(1, "Cost");
      public static readonly RouteCriteria Time = new RouteCriteria(2, "Time");
   }
}
