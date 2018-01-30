using System.Collections.Generic;

namespace DeliveryService.Domain.Services.Routing.Contracts {
   public interface IPathResult {
      IEnumerable<uint> GetReversePath();
      IEnumerable<uint> GetPath();
      bool IsFounded { get; }
      int Distance { get; }
      uint FromNode { get; }
      uint ToNode { get; }
   }
}
