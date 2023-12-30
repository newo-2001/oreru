namespace Oreru.Domain.Osu;

/// <summary>
/// An indidual object in an <see cref="Oreru.Domain.Osu.OsuStoryboard"/>.
/// Usually a single sprite with its associated <see cref="Oreru.Domain.Osu.OsuStoryboardCommand"/>s.
/// </summary>
public class OsuStoryboardObject
{
    public required OsuStoryboardLayer Layer { get; init; }
    public required string FilePath { get; set; }
    public required Origin Origin { get; set; }
    public required Vector2 InitialPosition { get; set; }
    public List<OsuStoryboardCommand> Commands { get; set; } = [];
}
