using Oreru.Domain;
using Oreru.Domain.Containers;
using Oreru.Domain.Storyboards;

namespace Oreru.Osu;

/// <summary>
/// An indidual object in an <see cref="OsuStoryboard"/>.
/// Usually a single sprite with its associated <see cref="OsuStoryboardCommand"/>s.
/// </summary>
public class OsuStoryboardObject : ITemporal
{
    public required StoryboardLayer Layer { get; init; }
    public required string FilePath { get; set; }
    public required Origin Origin { get; set; }
    public required Vector2 InitialPosition { get; set; }
    public Timeline<OsuStoryboardCommand> Commands { get; set; } = [];
    public Interval Interval => Commands.Interval;
}
