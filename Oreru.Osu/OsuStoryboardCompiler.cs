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
            child.Commands.AddRange(effect.Commands.SelectMany(CompileCommand));
        }

        return objects;
    }

    private IEnumerable<OsuStoryboardCommand> CompileCommand(Command command)
    {
        return command switch
        {
            Command.Composite(var composite) => composite.Children.SelectMany(CompileCommand),
            Command.Primitive(var primitive) => [
                primitive switch
                {
                    PrimitiveCommand.Fade fade => new OsuStoryboardCommand.Fade
                    {
                        EasingFunction = fade.EasingFunction,
                        Interval = fade.Interval,
                        Opacity = fade.Opacity
                    },
                    PrimitiveCommand.Move move => new OsuStoryboardCommand.Move
                    {
                        EasingFunction = move.EasingFunction,
                        Interval = move.Interval,
                        Position = move.Position,
                    },
                    PrimitiveCommand.Scale scale => new OsuStoryboardCommand.VectorScale
                    {
                        EasingFunction = scale.EasingFunction,
                        Interval = scale.Interval,
                        Scalars = scale.Scalars,
                    },
                    PrimitiveCommand.Rotate rotation => new OsuStoryboardCommand.Rotate
                    {
                        EasingFunction = rotation.EasingFunction,
                        Interval = rotation.Interval,
                        Angle = rotation.Angle,
                    },
                    PrimitiveCommand.Flip flip when flip.Axis == Axis.Horizontal => new OsuStoryboardCommand.FlipHorizontally
                    {
                        EasingFunction = flip.EasingFunction,
                        Interval = flip.Interval
                    },
                    PrimitiveCommand.Flip flip when flip.Axis == Axis.Vertical => new OsuStoryboardCommand.FlipVertically
                    {
                        EasingFunction = flip.EasingFunction,
                        Interval = flip.Interval
                    },
                    PrimitiveCommand.ChangeColor color => new OsuStoryboardCommand.ChangeColor
                    {
                        EasingFunction = color.EasingFunction,
                        Color = color.Color,
                        Interval = color.Interval
                    },
                    PrimitiveCommand.UseAdditiveBlending blend => new OsuStoryboardCommand.UseAdditiveColorBlending
                    {
                        EasingFunction = blend.EasingFunction,
                        Interval = blend.Interval,
                    },
                    _ => throw new InvalidOperationException("Encountered unknown primitive command")
                }
            ],
            _ => throw new InvalidOperationException("Encounted unknown command type")
        };
    }
}
