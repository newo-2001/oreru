namespace Oreru.Domain;

/// <summary>
/// A temporal object that only exists for a defined time <see cref="Domain.Interval"/>.
/// </summary>
public interface ITemporal : IComparable<ITemporal>
{
    public Interval Interval { get; }

    int IComparable<ITemporal>.CompareTo(ITemporal? other)
    {
        return other is not null ? Interval.CompareTo(other.Interval) : -1;
    }
}
