namespace Oreru.Osu;

using Oreru.Domain.Storyboards;

/// <summary>
/// Compiles an oreru! <see cref="Storyboard"/>
/// to a native <see cref="OsuStoryboard"/>.
/// </summary>
public class OsuStoryboardCompiler
{
    public OsuStoryboard Compile(Storyboard storyboard)
    {
        return new OsuStoryboard
        {
            Objects = [..storyboard.Effects.SelectMany(CompileEffect)]
        };
    }

    private static IEnumerable<OsuStoryboardObject> CompileEffect(Effect effect)
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
            var state = new OsuStoryboardObjectState(child);
            child.Commands.AddRange(effect.Commands.Select(command => CompileCommand(command, state)));
        }

        return objects;
    }

    private static OsuStoryboardCommand CompileCommand(StoryboardCommand command, OsuStoryboardObjectState state)
    {
        OsuStoryboardCommand compiled = command switch
        {
            StoryboardCommand.Move(var interval, var offset) => new OsuStoryboardCommand.Move(interval, new(state.Position, state.Position + offset)),
            StoryboardCommand.Fade(var interval, var multiplier) => new OsuStoryboardCommand.Fade(interval, new(state.Opacity, state.Opacity * multiplier)),
            StoryboardCommand.Scale(var interval, var (x, y)) => new OsuStoryboardCommand.VectorScale(interval, new(state.ScalingFactor, (state.ScalingFactor.X * x, state.ScalingFactor.Y * y))),
            StoryboardCommand.Rotate(var interval, var angle) => new OsuStoryboardCommand.Rotate(interval, new(state.Angle, state.Angle + angle)),
            StoryboardCommand.ChangeColor(var interval,var color) => new OsuStoryboardCommand.ChangeColor(interval, new(state.Color, color)),
            StoryboardCommand.Flip(var interval, var axis) => new OsuStoryboardCommand.Flip(interval, axis),
            StoryboardCommand.UseAdditiveBlending(var interval) => new OsuStoryboardCommand.UseAdditiveBlending(interval),
            _ => throw new ArgumentException("Unknown command encountered")
        };

        state.Apply(command);
        
        compiled.EasingFunction = command.EasingFunction;
        return compiled;
    }
}
