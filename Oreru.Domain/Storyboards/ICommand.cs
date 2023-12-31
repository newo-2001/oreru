namespace Oreru.Domain.Storyboards;

/// <summary>
/// A command represents a transformation performed on one or more objects.
/// These transformations are composed from other <see cref="ICommand"/>s.
/// The simplest transformations are <see cref="PrimitiveCommand"/>s.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// The interval during which the command is active
    /// </summary>
    public Interval Interval { get; }
}
