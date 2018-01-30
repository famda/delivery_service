using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Services.WebApi.Model {
   public class RouteFilter {
      [Required(ErrorMessage = "The origin is required.")]
      public string Origin { get; set; }
      [Required(ErrorMessage = "The destination is required.")]
      public string Destination { get; set; }
      [Required(ErrorMessage = "The criteria is required.")]
      public string Criteria { get; set; }
   }
}
