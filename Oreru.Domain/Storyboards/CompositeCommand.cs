using Oreru.Domain.Containers;

namespace Oreru.Domain.Storyboards;

/// <summary>
/// A composite command is a command composed of multiple other commands.
/// </summary>
public class CompositeCommand : ITemporal
{
    public required Timeline<Command> Children { get; init; }
    public Interval Interval => Children.Interval;
}
