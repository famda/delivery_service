using System.Collections.Generic;

namespace DeliveryService.Domain.Services.Extensions {
   internal static class MapExtensions {
      public static T MapTo<T>(this object value) {
         return AutoMapper.Mapper.Map<T>(value);
      }

      public static IEnumerable<T> EnumerableTo<T>(this object value) {
         return AutoMapper.Mapper.Map<IEnumerable<T>>(value);
      }
   }
}
