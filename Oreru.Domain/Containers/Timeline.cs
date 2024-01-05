namespace Oreru.Domain.Containers;

/// <summary>
/// Sorted container type for <see cref="ITemporal"/> objects.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Timeline<T> : SortedList<T>, ITemporal where T: ITemporal
{
    public Timeline() : base(Comparer<T>.Create((a, b) => a.CompareTo(b))) {}

    public Interval Interval => Interval.Super(this.First().Interval, this.Last().Interval);
}
