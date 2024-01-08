using System.Diagnostics.CodeAnalysis;

namespace Oreru.Domain;

/// <summary>
/// A time interval with a start time (inclusive) and end time (exclusive).
/// </summary>
public struct Interval : IComparable<Interval>
{
    public required TimeSpan StartTime { get; set; }
    public required TimeSpan EndTime { get; set; }
    public readonly TimeSpan Duration => EndTime - StartTime;

    public Interval() {}

    [SetsRequiredMembers]
    public Interval(TimeSpan startTime, TimeSpan endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public static Interval FromDuration(TimeSpan startTime, TimeSpan duration) => new Interval
    {
        StartTime = startTime,
        EndTime = startTime + duration
    };

    public readonly bool Contains(TimeSpan time) => time >= StartTime && time < EndTime;

    public readonly bool Contains(Interval other) => other.StartTime >= StartTime && other.EndTime <= EndTime;

    public readonly int CompareTo(Interval other)
    {
        var order = StartTime.CompareTo(other.StartTime);
        return order != 0 ? order : other.EndTime.CompareTo(EndTime);
    }
    
    /// <summary>
    /// Computes the "super" interval, that is,
    /// the smallest interval that contains all the given ones.
    /// </summary>
    public static Interval Super(IEnumerable<Interval> intervals) => new Interval
    {
        StartTime = intervals.Select(x => x.StartTime).Min(),
        EndTime = intervals.Select(x => x.EndTime).Max()
    };

    /// <inheritdoc cref="Super(IEnumerable{Interval})"/>
    public static Interval Super(params Interval[] intervals) => Super(intervals.AsEnumerable());
}
