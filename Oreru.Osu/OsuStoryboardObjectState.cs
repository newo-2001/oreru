using Oreru.Domain;
using Oreru.Domain.Storyboards;
using System.Diagnostics.CodeAnalysis;

namespace Oreru.Osu;

/// <summary>
/// The state of a simulated <see cref="OsuStoryboardObject"/>.
/// </summary>
public class OsuStoryboardObjectState
{
    public required Vector2 Position { get; set; }
    public Vector2 ScalingFactor { get; set; } = Vector2.One;
    public double Opacity { get; set; } = 1;
    public double Angle { get; set; } = 0;
    public Color Color { get; set; } = new Color(255, 255, 255);

    [SetsRequiredMembers]
    public OsuStoryboardObjectState(OsuStoryboardObject obj)
    {
        Position = obj.InitialPosition;
    }
    
    /// <summary>
    /// Update the state of the object by applying a command to it.
    /// </summary>
    /// <param name="command">The command to apply</param>
    public void Apply(StoryboardCommand command)
    {
        switch (command)
        {
            case StoryboardCommand.Move(_, var offset):
                Position += offset;
                break;
            case StoryboardCommand.Fade(_, var multiplier):
                Opacity *= multiplier;
                break;
            case StoryboardCommand.Scale(_, var scale):
                ScalingFactor = new Vector2(ScalingFactor.X * scale.X, ScalingFactor.Y * scale.Y);
                break;
            case StoryboardCommand.ChangeColor(_, var color):
                // Don't know what to do with this to be honest
                // There is not 'one' correct way to blend colors
                Color = color;
                break;
        };
    }
}
