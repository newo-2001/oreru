namespace Oreru.Osu;

using Oreru.Domain;
using Oreru.Domain.Storyboards;

/// <summary>
/// Compiles an oreru! <see cref="Storyboard"/>
/// to a native <see cref="OsuStoryboard"/>.
/// </summary>
public class OsuStoryboardCompiler
{
    public OsuStoryboard Compile(Storyboard storyboard)
    {
        var compiledStoryboard = new OsuStoryboard();
        compiledStoryboard.AddObjects(storyboard.Effects.SelectMany(CompileEffect));

        return compiledStoryboard;
    }

    private IEnumerable<OsuStoryboardObject> CompileEffect(Effect effect)
    {
        var objects = effect.Objects.Select(obj => new OsuStoryboardObject
        {
            FilePath = obj.ImagePath,
            InitialPosition = obj.InitialPosition,
            Layer = obj.Layer,
            Origin = obj.Origin,
        }).Concat(effect.Children.SelectMany(CompileEffect))
            .ToList();

        // Flatten the inherited transformations from the parent
        foreach (var child in objects)
        {
            // This does not actually do what is intended.
            // i.e. when multiple move commands overlap,
            // their offsets should be merged.
            child.Commands.AddRange(effect.Commands);
        }

        return objects;
    }
}
