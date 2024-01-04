using Oreru.Domain.Containers;

namespace Oreru.Domain.Storyboards;

/// <summary>
/// The central building block in oreru! storyboards.
/// Effects form a hierarchy, not unlike a scenegraph,
/// and contain objects and their associated transformations.
/// </summary>
public class Effect : ITemporal
{
    public Timeline<Command> Commands { get; init; } = [];
    public Timeline<Effect> Children { get; init; } = [];
    public List<StoryboardObject> Objects { get; init; } = [];
    public Interval Interval => Interval.Super(Commands.Interval, Children.Interval);
}
