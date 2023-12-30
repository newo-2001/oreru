namespace Oreru.Domain.Osu;

/// <summary>
/// A storyboard stored the way osu! sees it.
/// This object can be manipulated and serialized to a file
/// to programmatically change a storyboard.
/// </summary>
public class OsuStoryboard
{
    public List<OsuStoryboardObject> BackgroundLayer { get; set; } = [];
    public List<OsuStoryboardObject> PassLayer { get; set; } = [];
    public List<OsuStoryboardObject> FailLayer { get; set; } = [];
    public List<OsuStoryboardObject> ForegroundLayer { get; set; } = [];

    public List<OsuStoryboardObject> GetLayer(OsuStoryboardLayer layer)
    {
        return layer switch
        {
            OsuStoryboardLayer.Background => BackgroundLayer,
            OsuStoryboardLayer.Fail => FailLayer,
            OsuStoryboardLayer.Pass => PassLayer,
            OsuStoryboardLayer.Foreground => ForegroundLayer,
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
