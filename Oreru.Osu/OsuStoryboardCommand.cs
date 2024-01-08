using Oreru.Domain;
using Oreru.Domain.Storyboards;

namespace Oreru.Osu;

/// <summary>
/// The commands as declared in the <c>.osb</c> file.
/// Note that these values are absolute and not relative
/// like the values of <see cref="StoryboardCommand"/>.
/// </summary>
public abstract record OsuStoryboardCommand(Interval Interval) : ITemporal
{
    public EasingFunction EasingFunction { get; set; } = EasingFunction.Linear;
    
    /// <param name="Opacity">The opacities to fade between, opacity is a value between 0 (transparent) and 1 (opaque).</param>
    public sealed record Fade(Interval Interval, Interpolated<double> Opacity) : OsuStoryboardCommand(Interval) {}

    /// <param name="Position">The target X coordinate to move in osupixels (0 to 512).</param>
    public sealed record MoveX(Interval Interval, Interpolated<double> Position) : OsuStoryboardCommand(Interval) {}

    /// <param name="Position">The target Y coordinate in osupixels (0 to 384).</param>
    public sealed record MoveY(Interval Interval, Interpolated<double> Position) : OsuStoryboardCommand(Interval) {}

    /// <param name="Position">The target position to move in osupixels (0, 0) to (512, 384).</param>
    public sealed record Move(Interval Interval, Interpolated<Vector2> Position) : OsuStoryboardCommand(Interval) {}

    /// <param name="Scalar">The new scaling factor.</param>
    public sealed record Scale(Interval Interval, Interpolated<double> Scalar) : OsuStoryboardCommand(Interval) {}

    /// <param name="Scalars">The new scaling factors along each of the axis.</param>
    public sealed record VectorScale(Interval Interval, Interpolated<Vector2> Scalars) : OsuStoryboardCommand(Interval) {}
    
    /// <param name="Angle">
    /// The angle of the rotation in radians.
    /// If this exceeds 2π, it represents multiple rotations.
    /// </param>
    public sealed record Rotate(Interval Interval, Interpolated<double> Angle) : OsuStoryboardCommand(Interval) {}

    /// <param name="Axis">The axis to flip along.</param>
    public sealed record Flip(Interval Interval, Axis Axis) : OsuStoryboardCommand(Interval) {}
    
    /// <param name="Color">The color to fade to, typically given in RGB (0-255).</param>
    public sealed record ChangeColor(Interval Interval, Interpolated<Color> Color) : OsuStoryboardCommand(Interval) {}
    public sealed record UseAdditiveBlending(Interval Interval) : OsuStoryboardCommand(Interval) {}
}
