using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain.Services.Model {
   public class RouteDto {
      public string Id { get; set; }
      [Required(ErrorMessage = "The origin is required.")]
      public PlaceDto Origin { get; set; }
      [Required(ErrorMessage = "The destination is required.")]
      public PlaceDto Destination { get; set; }
      [Required(ErrorMessage = "The time cost is required.")]
      public int Time { get; set; }
      [Required(ErrorMessage = "The cost is required.")]
      public int Cost { get; set; }
   }
}
