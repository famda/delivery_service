using System;
using System.Linq;
using DeliveryService.Domain.Services;
using DeliveryService.Services.WebApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.WebApi.Controllers {
   [Produces("application/json")]
   [Route("api/routes")]
   public class RoutesController : Controller {
      private readonly IRouteService _service;

      public RoutesController(IRouteService service) { _service = service; }

      [HttpGet]
      [Route("best")]
      public ActionResult Get([FromQuery]RouteFilter filter) {
         try {
            if (ModelState.IsValid)
               return new JsonResult(new ApiResponse { Success = true, Data = _service.GetBestRoute(filter.Origin, filter.Destination, filter.Criteria) });
            var errors = ModelState.Keys
             .SelectMany(key => ModelState[key].Errors.Select(x => new { Property = key, Error = x.ErrorMessage })).ToList();
            return new JsonResult(new ApiResponse { Success = false, Data = errors });
         } catch (Exception ex) {
            return new JsonResult(new ApiResponse { Success = false, Data = ex.Message });
         }
      }
   }
}
