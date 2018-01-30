using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryService.Domain.Services.IoC {
   public static class Module {
      public static Dictionary<Type, Type> GetTypes() => new Dictionary<Type, Type> {
            { typeof(IRouteService), typeof(RouteService) },
      };

      public static void Configure(IServiceCollection services, IConfiguration configuration) {
         GetTypes().ToList().ForEach(x => services.AddScoped(x.Key, x.Value));
         //Initialize mapping profiles Dto vs Domain Model and vice-versa
         AutoMapper.Mapper.Initialize((cfg) => { cfg.AddProfiles(typeof(Extensions.MappingProfile)); });
      }
   }
}
