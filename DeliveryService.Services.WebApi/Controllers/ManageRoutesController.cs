using System;
using System.Linq;
using DeliveryService.Domain.Services;
using DeliveryService.Domain.Services.Model;
using DeliveryService.Services.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.WebApi.Controllers {
   [Produces("application/json")]
   [Route("api/routes")]
   [Authorize("AdminOnly")]
   public class ManageRoutesController : Controller {
      private readonly IRouteService _service;
      public ManageRoutesController(IRouteService service) { _service = service; }

      [HttpGet]
      public ActionResult Get() => new JsonResult(_service.GetAllRoutes());

      [HttpPost]
      public IActionResult Post([FromBody] RouteDto routeDto) {
         try {
            if (ModelState.IsValid)
               return new JsonResult(new ApiResponse { Success = true, Data = _service.AddRoute(routeDto) });
            var errors = ModelState.Keys
             .SelectMany(key => ModelState[key].Errors.Select(x => new { Property = key, Error = x.ErrorMessage })).ToList();
            return new JsonResult(new ApiResponse { Success = false, Data = errors });
         } catch (Exception ex) {
            return new JsonResult(new ApiResponse { Success = false, Data = ex.Message });
         }
      }

      [HttpPut]
      public ActionResult Put(string id, [FromBody]RouteDto route) {
         //Verificar o ID e modificar
         try {
            if (ModelState.IsValid)
               return new JsonResult(new ApiResponse { Success = true, Data = _service.UpdateRoute(id, route) });
            var errors = ModelState.Keys
               .SelectMany(key => ModelState[key].Errors.Select(x => new { Property = key, Error = x.ErrorMessage })).ToList();
            return new JsonResult(new ApiResponse { Success = false, Data = errors });
         } catch (Exception ex) {
            return new JsonResult(new ApiResponse { Success = false, Data = ex.Message });
         }
      }

      [HttpDelete("{id}")]
      public ActionResult Delete(string id) {
         try {
            _service.RemoveRoute(id);
            return new JsonResult(new ApiResponse { Success = true });
         } catch (Exception ex) {
            return new JsonResult(new ApiResponse { Success = false, Data = ex.Message });
         }
      }
   }
}