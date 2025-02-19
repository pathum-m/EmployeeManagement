﻿namespace EmployeeManagement.Domain.Base;

public abstract class Entity : IEquatable<Entity>
{
    protected Entity(Guid id) => Id = id;

    public Guid Id { get; private init; }

    public static bool operator ==(Entity? first, Entity? second) => first is not null && second is not null && first.Equals(second);

    public static bool operator !=(Entity? first, Entity second) => !(first == second);

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

        if (obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public bool Equals(Entity? other)
    {
        if (other == null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return other.Id == Id;
    }

    public override int GetHashCode() => Id.GetHashCode();
}
