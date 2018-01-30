using DeliveryService.Domain.Core.Model;
using DeliveryService.Domain.Core.Repositories;
using DeliveryService.Domain.Core.Services;
using System;

namespace DeliveryService.Domain.Services {
   public abstract class ServiceBase<T> : IServiceBase<T> where T : class, IEntityBase {
      internal readonly IUnitOfWork _unitOfWork;
      internal readonly IReadUnit _readUnit;
      internal readonly IReadRepository<T> _readRepository;
      internal readonly IWriteRepository<T> _writeRepository;

      public ServiceBase(IUnitOfWork unitOfWork, IReadUnit readUnit) {
         _unitOfWork = unitOfWork ?? throw new ArgumentException("Unit of Work");
         _readUnit = readUnit ?? throw new ArgumentException("Read Unit");
         _writeRepository = _unitOfWork.WriteRepository<T>();
         _readRepository = _readUnit.ReadRepository<T>();
      }
   }
}
