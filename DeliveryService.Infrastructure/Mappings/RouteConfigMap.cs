using DeliveryService.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Infrastructure.Mappings {
   public class RouteConfigMap : IEntityTypeConfiguration<Route> {
      public void Configure(EntityTypeBuilder<Route> builder) {

         builder.ToTable("routes");
         builder.HasKey(x => x.Id);

         builder.HasOne(x => x.Origin);
         builder.HasOne(x => x.Destination);

         builder.HasIndex(x => new { x.OriginId, x.DestinationId }).IsUnique().HasName("Index_PlacePair");

      }
   }
}
