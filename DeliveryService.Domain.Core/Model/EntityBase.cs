using System;

namespace DeliveryService.Domain.Core.Model {
   public abstract class EntityBase<T> : IEntityBase, IEquatable<EntityBase<T>> {
      private T _id;
      public T Id { get { return _id; } }
      protected EntityBase() { }
      public EntityBase(T id) { _id = id; }
      public override bool Equals(object entity) => entity != null && entity is EntityBase<T> && this == (EntityBase<T>)entity;
      public override int GetHashCode() { return this.Id.GetHashCode(); }
      public static bool operator ==(EntityBase<T> entity1, EntityBase<T> entity2) {
         if ((object)entity1 == null && (object)entity2 == null) { return true; }
         if ((object)entity1 == null || (object)entity2 == null) { return false; }
         if (entity1.Id.ToString() == entity2.Id.ToString()) { return true; }
         return false;
      }
      public static bool operator !=(EntityBase<T> entity1, EntityBase<T> entity2) {
         return (!(entity1 == entity2));
      }
      public bool Equals(EntityBase<T> other) {
         if (other == null) { return false; }
         return this.Id.Equals(other.Id);
      }
   }
}
