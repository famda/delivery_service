using DeliveryService.Domain.Core.Model;
using DeliveryService.Domain.Core.Repositories;
using DeliveryService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryService.Infrastructure.Repositories {
   public class UnitOfWork : IUnitOfWork, IDisposable {
      private const string CONNECTIONSTRING_KEY = "DeliveryServiceDb";
      private readonly string _connectionString;
      private readonly DeliveryServiceDbContext _context;
      private Dictionary<string, object> repositories;

      public UnitOfWork(IConfiguration configuration) {
         var connectionString = configuration.GetConnectionString(CONNECTIONSTRING_KEY);
         if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException("Connection string missing");
         _connectionString = connectionString;
         _context = new DeliveryServiceDbContext(configuration);
      }

      public IWriteRepository<T> WriteRepository<T>() where T : class, IEntityBase {
         if (repositories == null)
            repositories = new Dictionary<string, object>();
         var type = typeof(T).Name;
         if (!repositories.ContainsKey(type)) {
            var repositoryType = typeof(WriteRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
            repositories.Add(type, repositoryInstance);
         }
         return (WriteRepository<T>)repositories[type];
      }

      public void Commit() {
         bool saveFailed;
         do {
            saveFailed = false;
            try {
               _context.SaveChanges();
            } catch (DbUpdateConcurrencyException ex) {
               saveFailed = true;
               var entry = ex.Entries.Single();
               entry.OriginalValues.SetValues(entry.GetDatabaseValues());
            }
         } while (saveFailed);
      }

      public async void CommitAsync() {
         bool saveFailed;
         do {
            saveFailed = false;
            try {
               await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
               saveFailed = true;
               var entry = ex.Entries.Single();
               entry.OriginalValues.SetValues(entry.GetDatabaseValues());
            }
         } while (saveFailed);
      }

      public void RollBack() {
         var changedEntriesCopy = _context.ChangeTracker.Entries()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted).ToList();
         foreach (var entity in changedEntriesCopy)
            _context.Entry(entity.Entity).State = EntityState.Detached;
      }
      public void Dispose() => _context.Dispose();
   }
}
