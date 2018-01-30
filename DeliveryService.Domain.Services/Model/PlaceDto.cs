using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain.Services.Model {
   public class PlaceDto {
      public string Id { get; set; }
      [Required(ErrorMessage = "The description is required.")]
      public string Description { get; set; }
   }
}
