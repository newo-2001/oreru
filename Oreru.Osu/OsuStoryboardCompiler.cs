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

    private IEnumerable<OsuStoryboardObject> CompileEffect(IEffect effect)
    {
        return effect switch
        {
            StoryboardObject obj => [
                new OsuStoryboardObject
                {
                    Commands = obj.Commands.SelectMany(CompileCommand).ToList(),
                    FilePath = obj.ImagePath,
                    InitialPosition = obj.InitialPosition,
                    Layer = obj.Layer,
                    Origin = obj.Origin,
                }
            ],
            EffectGroup group => CompileEffectGroup(group),
            _ => throw new InvalidOperationException("Encountered unknown effect type")
        };
    }

    private IEnumerable<OsuStoryboardCommand> CompileCommand(ICommand command)
    {
        return command switch
        {
            ICompositeCommand composite => composite.Children.SelectMany(CompileCommand),
            PrimitiveCommand primitive => [
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

    private IEnumerable<OsuStoryboardObject> CompileEffectGroup(EffectGroup group)
    {
        var children = group.Children.SelectMany(CompileEffect).ToList();
        var commands = group.Commands.SelectMany(CompileCommand).ToList();

        // Flatten the inherited transformations from the parent
        foreach (var child in children)
        {
            // This does not actually do what is inteded.
            // i.e. when multiple move commands overlap,
            // their offsets should be merged.
            child.Commands.AddRange(commands);
        }

        return children;
    }
}
