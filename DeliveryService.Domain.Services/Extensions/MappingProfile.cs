using DeliveryService.Domain.Model;
using DeliveryService.Domain.Services.Model;
using System;

namespace DeliveryService.Domain.Services.Extensions {
   internal class MappingProfile : AutoMapper.Profile {
      public MappingProfile() {

         CreateMap<Place, PlaceDto>().ConstructUsing(x => new PlaceDto() {
            Id = x.Id.ToString(),
            Description = x.Description
         });

         CreateMap<PlaceDto, Place>().ConstructUsing(x => {
            if (string.IsNullOrEmpty(x.Id) || x.Id.Equals(default(Guid)))
               x.Id = Guid.NewGuid().ToString();
            return new Place(Guid.Parse(x.Id), x.Description);
         });

         CreateMap<Route, RouteDto>().ConstructUsing(x => new RouteDto() {
            Id = x.Id.ToString(),
            Origin = x.Origin.MapTo<PlaceDto>(),
            Destination = x.Destination.MapTo<PlaceDto>(),
            Cost = x.Cost,
            Time = x.Time
         });

         CreateMap<RouteDto, Route>().ConstructUsing(x => {
            if (string.IsNullOrEmpty(x.Id) || x.Id.Equals(default(Guid)))
               x.Id = Guid.NewGuid().ToString();
            return new Route(Guid.Parse(x.Id), x.Origin.MapTo<Place>(), x.Destination.MapTo<Place>(), x.Time, x.Cost);
         });
      }
   }
}
