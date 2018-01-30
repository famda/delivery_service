using DeliveryService.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Infrastructure.Mappings {
   public class PlaceConfigMap : IEntityTypeConfiguration<Place> {
      public void Configure(EntityTypeBuilder<Place> builder) {
         builder.ToTable("places");
         builder.HasKey(x => x.Id);
         builder.HasIndex(x => x.Description).IsUnique();

         builder.HasMany<Route>().WithOne(x=>x.Origin).OnDelete(DeleteBehavior.Restrict);
         builder.HasMany<Route>().WithOne(x=>x.Destination).OnDelete(DeleteBehavior.Restrict);

      }
   }
}
