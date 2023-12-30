namespace Oreru.Domain;

/// <summary>
/// A time interval with a start time (inclusive) and end time (exclusive).
/// </summary>
public struct Interval
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public readonly TimeSpan Duration => EndTime - StartTime;

    public Interval() {}
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
}
