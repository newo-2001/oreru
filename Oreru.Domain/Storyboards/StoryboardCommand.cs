namespace Oreru.Domain.Storyboards;

/// <summary>
/// Storyboard commands are the most basic building block of transformations in oreru!
/// These represent commands natively supported by osu!
/// The only difference is that the values in oreru! commands are relative,
/// to the current state of the object whereas osu! commands are absolute.
/// </summary>
public abstract record StoryboardCommand(Interval Interval) : ITemporal
{
    public EasingFunction EasingFunction { get; set; } = EasingFunction.Linear;
    
    /// <param name="OpacityMultiplier">The multiplier to apply to the opacity.</param>
    public sealed record Fade(Interval Interval, double OpacityMultiplier) : StoryboardCommand(Interval) {}

    /// <param name="Position">The offset to move in osupixels (the playfield area is 512x384.)</param>
    public sealed record Move(Interval Interval, Vector2 Position) : StoryboardCommand(Interval) {}

    /// <param name="Scalars">The factor by which to scale along each axis.</param>
    public sealed record Scale(Interval Interval, Vector2 Scalars) : StoryboardCommand(Interval) {}
    
    /// <param name="Angle">
    /// The angle of the rotation in radians.
    /// If this exceeds 2π, it represents multiple rotations.
    /// </param>
    public sealed record Rotate(Interval Interval, double Angle) : StoryboardCommand(Interval) {}

    /// <param name="Axis">The axis to flip along.</param>
    public sealed record Flip(Interval Interval, Axis Axis) : StoryboardCommand(Interval) {}
    
    /// <param name="Color">The color to fade to, typically given in RGB (0-255).</param>
    public sealed record ChangeColor(Interval Interval, Color Color) : StoryboardCommand(Interval) {}
    public sealed record UseAdditiveBlending(Interval Interval) : StoryboardCommand(Interval) {}
}
