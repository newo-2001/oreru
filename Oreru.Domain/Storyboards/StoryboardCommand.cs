namespace Oreru.Domain.Storyboards;

/// <summary>
/// Primitive commands are the most basic building block of transformations in oreru!
/// These represent commands natively supported by osu!
/// They can be composed using <see cref="CompositeCommand"/>.
/// </summary>
public abstract record StoryboardCommand : ITemporal
{
    public required Interval Interval { get; init; }
    public EasingFunction EasingFunction { get; set; } = EasingFunction.Linear;
    
    /// <param name="Opacity">The opacities to fade between, opacity is a value between 0 (transparent) and 1 (opaque).</param>
    public sealed record Fade(Interpolated<double> Opacity) : StoryboardCommand {}

    /// <param name="Position">The positions to move between in osupixels (0 to 512).</param>
    public sealed record MoveX(Interpolated<double> Position) : StoryboardCommand {}

    /// <param name="Position">The positions to move between in osupixels (0 to 384).</param>
    public sealed record MoveY(Interpolated<double> Position) : StoryboardCommand {}

    /// <param name="Position">The positions to move between in osupixels (0, 0) to (512, 384).</param>
    public sealed record Move(Interpolated<Vector2> Position) : StoryboardCommand {}

    /// <param name="Scalar">The scaling factor.</param>
    public sealed record Scale(Interpolated<double> Scalar) : StoryboardCommand {}

    /// <param name="Scalars">The scaling factors along each of the axis.</param>
    public sealed record VectorScale(Interpolated<Vector2> Scalars) : StoryboardCommand {}
    
    /// <param name="Angle">
    /// The angle of the rotation in radians.
    /// If this exceeds 2π, it represents multiple rotations.
    /// </param>
    public sealed record Rotate(Interpolated<double> Angle) : StoryboardCommand {}

    /// <param name="Axis">The axis to flip along.</param>
    public sealed record Flip(Axis Axis) : StoryboardCommand {}
    
    /// <param name="Color">The colors to fade between, typically given in RGB (0-255).</param>
    public sealed record ChangeColor(Interpolated<Color> Color) : StoryboardCommand {}
    public sealed record UseAdditiveBlending : StoryboardCommand {}

    private StoryboardCommand() {}
}
