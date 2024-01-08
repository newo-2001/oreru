using Oreru.Domain.Containers;
using Oreru.Domain.Storyboards;

namespace Oreru.Osu;

/// <summary>
/// A storyboard stored the way osu! sees it.
/// This object can be used to programmatically change a storyboard.
/// </summary>
public class OsuStoryboard
{
    public Timeline<OsuStoryboardObject> Objects { get; init; } = [];

    public IEnumerable<OsuStoryboardObject> GetLayer(StoryboardLayer layer) => Objects.Where(x => x.Layer == layer);
}
