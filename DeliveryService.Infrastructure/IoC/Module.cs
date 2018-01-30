using DeliveryService.Domain.Core.Repositories;
using DeliveryService.Domain.Model;
using DeliveryService.Infrastructure.Context;
using DeliveryService.Infrastructure.Initializers;
using DeliveryService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DeliveryService.Infrastructure.IoC {
   public static class Module {
      public static Dictionary<Type, Type> GetTypes() => new Dictionary<Type, Type> {
             { typeof(DeliveryServiceDbContext), typeof(DeliveryServiceDbContext) },
      };

      public static void Configure(IServiceCollection services, IConfiguration configuration) {
         services.AddScoped<IUnitOfWork>(s => new UnitOfWork(configuration));
         services.AddScoped<IReadUnit>(s => new ReadUnit(configuration));
         services.AddScoped(s => new DeliveryServiceDbContext(configuration));
         //foreach (var t in GetTypes()) { services.AddScoped(t.Key, t.Value); }
      }

      public static void InitializeDatabase(IServiceProvider serviceProvider) {
         using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
            var db = serviceScope.ServiceProvider.GetService<DeliveryServiceDbContext>();
            //Apenas em ambiente de desenvolvimento para testes
            //elimina a BD
            db.Database.EnsureDeleted();

            ///Assegura que a BD é criada
            if (db.Database.EnsureCreated()) {
               db.Set<Route>().AddRange(Seed.GetTestRoutes());
               db.SaveChanges();
            }
         }
      }
   }
}
