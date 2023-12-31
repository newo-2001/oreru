using Oreru.Domain.Storyboards;

namespace Oreru.Osu;

/// <summary>
/// A storyboard stored the way osu! sees it.
/// This object can be used to programmatically change a storyboard.
/// </summary>
public class OsuStoryboard
{
    public List<OsuStoryboardObject> BackgroundLayer { get; set; } = [];
    public List<OsuStoryboardObject> PassLayer { get; set; } = [];
    public List<OsuStoryboardObject> FailLayer { get; set; } = [];
    public List<OsuStoryboardObject> ForegroundLayer { get; set; } = [];

    public IEnumerable<OsuStoryboardObject> Objects => new List<OsuStoryboardObject>[] {
        BackgroundLayer,
        PassLayer,
        FailLayer,
        ForegroundLayer
    }.SelectMany(x => x);

    public List<OsuStoryboardObject> GetLayer(StoryboardLayer layer)
    {
        return layer switch
        {
            StoryboardLayer.Background => BackgroundLayer,
            StoryboardLayer.Fail => FailLayer,
            StoryboardLayer.Pass => PassLayer,
            StoryboardLayer.Foreground => ForegroundLayer,
            _ => throw new ArgumentException($"Invalid Storyboard Layer: {layer}")
        };
    }

    public void AddObject(OsuStoryboardObject obj)
    {
        GetLayer(obj.Layer).Add(obj);
    }

    public void AddObjects(IEnumerable<OsuStoryboardObject> objects)
    {
        foreach (var obj in objects)
        {
            AddObject(obj);
        }
    }
    public void RemoveObject(OsuStoryboardObject obj)
    {
        GetLayer(obj.Layer).Remove(obj);
    }
}
