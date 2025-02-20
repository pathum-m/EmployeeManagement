namespace EmployeeManagement.Domain.Base;

public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    protected Entity(TId id) => Id = id;

    public TId Id { get; private init; }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second) => first is not null && second is not null && first.Equals(second);

    public static bool operator !=(Entity<TId>? first, Entity<TId> second) => !(first == second);

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity<TId> entity)
        {
            return false;
        }

        return EqualityComparer<TId>.Default.Equals(entity.Id, Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other == null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return EqualityComparer<TId>.Default.Equals(other.Id, Id);
    }

    public override int GetHashCode() => Id.GetHashCode();
}
