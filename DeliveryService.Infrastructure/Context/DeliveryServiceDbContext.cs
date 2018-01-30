using DeliveryService.Domain.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;

namespace DeliveryService.Infrastructure.Context {
   internal class DeliveryServiceDbContext : DbContext {
      private const string CONNECTIONSTRING_KEY = "DeliveryServiceDb";
      private readonly string _connectionString;

      public DeliveryServiceDbContext(DbContextOptions<DeliveryServiceDbContext> options) : base(options) { }

      public DeliveryServiceDbContext(IConfiguration configuration) {
         var connectionString = configuration.GetConnectionString(CONNECTIONSTRING_KEY);
         if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException("Connection string missing");
         _connectionString = connectionString;
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
         if (!optionsBuilder.IsConfigured) { optionsBuilder.UseSqlServer(_connectionString); }
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder) {
         //Detect all classes that implements IEntityTypeConfiguration interface
         Assembly.GetExecutingAssembly().GetTypes()
             .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition && t.GetTypeInfo().ImplementedInterfaces
             .Any(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList()
             .ForEach(x => modelBuilder.ApplyConfiguration((dynamic)Activator.CreateInstance(x)));
                  base.OnModelCreating(modelBuilder);
      }

      public new DbSet<T> Set<T>() where T : class, IEntityBase { return base.Set<T>(); }
   }
}
