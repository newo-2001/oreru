using Oreru.Domain.Containers;

namespace Oreru.Domain.Storyboards;

/// <summary>
/// A storyboard stored the way oreru! sees it.
/// This object can be used to programmatically change an oreru! storyboard.
/// </summary>
public class Storyboard : ITemporal
{
    public Timeline<Effect> Effects { get; init; } = [];
    public Interval Interval => Effects.Interval;
}
