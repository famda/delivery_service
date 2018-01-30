using DeliveryService.Domain.Core.Services;
using DeliveryService.Domain.Model;
using DeliveryService.Domain.Services.Model;
using System.Collections.Generic;

namespace DeliveryService.Domain.Services {
   public interface IRouteService : IServiceBase<Route> {
      IEnumerable<RouteDto> GetAllRoutes();
      RouteDto AddRoute(RouteDto route);
      IEnumerable<RouteDto> AddRoutes(IEnumerable<RouteDto> routes);
      RouteDto UpdateRoute(string id, RouteDto route);
      void RemoveRoute(string id);
      IEnumerable<PlaceDto> GetBestRoute(string originId, string destinationId, string criteria);
   }
}
