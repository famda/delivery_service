using DeliveryService.Domain.Core.Repositories;
using DeliveryService.Domain.Model;
using DeliveryService.Domain.Services.Extensions;
using DeliveryService.Domain.Services.Model;
using DeliveryService.Domain.Services.Routing;
using DeliveryService.Domain.Services.Routing.Contracts;
using DeliveryService.Domain.Services.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryService.Domain.Services {
   public class RouteService : ServiceBase<Route>, IRouteService {

      public RouteService(IUnitOfWork unitOfWork, IReadUnit readUnit) : base(unitOfWork, readUnit) { }

      public IEnumerable<RouteDto> GetAllRoutes() =>
         _readRepository.GetAllIncluding(x => x.Destination, x => x.Origin).EnumerableTo<RouteDto>();

      public RouteDto AddRoute(RouteDto route) {
         var newRoute = route.MapTo<Route>();
         _writeRepository.Add(newRoute);
         _unitOfWork.Commit();
         return newRoute.MapTo<RouteDto>();
      }

      public IEnumerable<RouteDto> AddRoutes(IEnumerable<RouteDto> routes) {
         var newRoutes = routes.EnumerableTo<Route>();
         _writeRepository.AddRange(newRoutes);
         _unitOfWork.Commit();
         return newRoutes.EnumerableTo<RouteDto>();
      }

      public RouteDto UpdateRoute(string id, RouteDto route) {
         var item = _readRepository.Get(x => x.Id.Equals(Guid.Parse(id))).FirstOrDefault();
         if (item != null) {
            _writeRepository.Update(route.MapTo<Route>());
            _unitOfWork.Commit();
            return item.MapTo<RouteDto>();
         }
         throw new ArgumentException("The passed route doesn't exist.");
      }

      public void RemoveRoute(string id) {
         var item = _readRepository.Get(x => x.Id.Equals(Guid.Parse(id))).FirstOrDefault();
         if (item != null) {
            _writeRepository.Remove(item);
            _unitOfWork.Commit();
         }
         throw new ArgumentException("The passed route doesn't exist.");
      }

      public IEnumerable<PlaceDto> GetBestRoute(string originName, string destinationName, string criteria) {

         if (string.IsNullOrEmpty(originName) && string.IsNullOrEmpty(destinationName))
            throw new ArgumentException("The Origin and Destination are required fields.");

         var origin = _readUnit.ReadRepository<Place>().Get(x => x.Description.Equals(originName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
         var destination = _readUnit.ReadRepository<Place>().Get(x => x.Description.Equals(destinationName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

         if (origin == null)
            throw new InvalidOperationException("The Origin provided does not exist.");

         if (destination == null)
            throw new InvalidOperationException("The Destination provided does not exist.");

         //Retrieve filter criteria
         //if the passed criteria is null, the default value is Cost
         //if find a match will return a RouteCriteria type
         //if the passed value doesn't exist, will throw an exception
         RouteCriteria criteriaRoute = string.IsNullOrEmpty(criteria) ? RouteCriteria.Cost :
            RouteCriteria.FromDisplayName<RouteCriteria>(criteria);

         //retrieve routes and places from DB
         var routes = _readRepository.GetAllIncluding(x => x.Origin, x => x.Destination).ToList();
         var places = _readUnit.ReadRepository<Place>().GetAll().ToList();

         //create the graph network
         RoutingGraph<Place> graph = new RoutingGraph<Place>();
         places.ForEach(x => graph.AddNode(x));
         routes.ForEach(r => {
            int cost = RouteCriteria.Equals(criteriaRoute, RouteCriteria.Time) ? r.Time : r.Cost;
            graph.Connect(r.Origin, r.Destination, cost);
         });

         //processa o resultado
         var processor = new RouteProcessor<Place>(graph);
         IPathResult result = processor.Process(origin, destination);

         //build the result
         List<Place> resultados = new List<Place>();
         result.GetPath().ToList().ForEach(x => resultados.Add(graph[x].Item));
         return resultados.EnumerableTo<PlaceDto>();
      }
   }
}
