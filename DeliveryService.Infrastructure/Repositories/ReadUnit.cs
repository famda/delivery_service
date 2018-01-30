using DeliveryService.Domain.Core.Model;
using DeliveryService.Domain.Core.Repositories;
using DeliveryService.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DeliveryService.Infrastructure.Repositories {
   public class ReadUnit : IReadUnit, IDisposable {

      private const string CONNECTIONSTRING_KEY = "DeliveryServiceDb";
      private readonly string _connectionString;
      private readonly DeliveryServiceDbContext _context;
      private Dictionary<string, object> repositories;

      public ReadUnit(IConfiguration configuration) {
         var connectionString = configuration.GetConnectionString(CONNECTIONSTRING_KEY);
         if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException("Connection string missing");
         _connectionString = connectionString;
         _context = new DeliveryServiceDbContext(configuration);
      }

      public IReadRepository<T> ReadRepository<T>() where T : class, IEntityBase {
         if (repositories == null)
            repositories = new Dictionary<string, object>();
         var type = typeof(T).Name;
         if (!repositories.ContainsKey(type)) {
            var repositoryType = typeof(ReadRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            repositories.Add(type, repositoryInstance);
         }
         return (ReadRepository<T>)repositories[type];
      }

      public void Dispose() => _context.Dispose();
   }
}
