using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryService.IoC {
   public static class IoCConfiguration {

      public static void Configure(IConfiguration configuration, IServiceCollection services) {
         DeliveryService.Infrastructure.IoC.Module.Configure(services, configuration);
         DeliveryService.Domain.Services.IoC.Module.Configure(services, configuration);
      }

      public static void ConfigureDb(IServiceProvider serviceProvider) {
         DeliveryService.Infrastructure.IoC.Module.InitializeDatabase(serviceProvider);
      }

      private static void Configure(IServiceCollection services, Dictionary<Type, Type> types) =>
         types.ToList().ForEach(x => services.AddScoped(x.Key, x.Value));

   }
}
