using Oreru.Domain;

namespace Oreru.Osu;

/// <summary>
/// Functions that transform storyboards
/// to be more concise, idiomatic, performant or readable
/// without impacting their functionality.
/// Individual transformations can be composed together to fit your needs.
/// </summary>
public static class OsuStoryboardOptimizers
{
    public delegate void Transformation(OsuStoryboard storboard);

    public static void All(OsuStoryboard storyboard)
    {
        var transformations = new Transformation[] {
            RemoveCustomOrigins,
        };

        foreach (var transformation in transformations)
        {
            transformation(storyboard);
        }
    }
    
    /// <summary>
    /// Replaces all occurances of <see cref="Origin.Custom"/> with <see cref="Origin.TopLeft"/>
    /// as the former has been deprecated according to <see href="https://osu.ppy.sh/wiki/en/Storyboard/Scripting/Objects">the wiki</see>.
    /// </summary>
    public static void RemoveCustomOrigins(OsuStoryboard storyboard)
    {
        foreach (var obj in storyboard.Objects)
        {
            if (obj.Origin == Origin.Custom)
            {
                obj.Origin = Origin.TopLeft;
            }
        }
    }
}
