namespace Oreru.Domain.Storyboards;

/// <summary>
/// Primitive commands are the most basic building block of transformations in oreru!
/// These represent commands natively supported by osu!
/// </summary>
public abstract class PrimitiveCommand : ICommand
{
    public required Interval Interval { get; set; }
    public EasingFunction EasingFunction { get; set; } = EasingFunction.Linear;

    Interval ICommand.Interval => Interval;

    public class Fade : PrimitiveCommand
    {
        /// <summary>
        /// The opacities to fade between, opacity is a value between 0 (transparent) and 1 (opaque).
        /// </summary>
        public required Interpolated<double> Opacity { get; set; }
    }

    public class Move : PrimitiveCommand
    {
        /// <summary>
        /// The positions to move between in osupixels (0, 0) to (512, 384).
        /// </summary>
        public required Interpolated<Vector2> Position { get; set; }
    }

    public class Scale : PrimitiveCommand
    {
        /// <summary>
        /// The scaling factors along each of the axis.
        /// </summary>
        public required Interpolated<Vector2> Scalars { get; set; }
    }

    public class Rotate : PrimitiveCommand
    {
        /// <summary>
        /// The angle of the rotation in radians.
        /// If this exceeds 2π, it represents multiple rotations.
        /// </summary>
        public required Interpolated<double> Angle { get; set; }
    }

    public class Flip : PrimitiveCommand
    {
        /// <summary>
        /// The axis to flip along.
        /// </summary>
        public required Axis Axis { get; set; }
    }

    public class ChangeColor : PrimitiveCommand
    {
        /// <summary>
        /// The colors to fade between, typically given in RGB (0-255).
        /// </summary>
        public required Interpolated<Color> Color { get; set; }
    }

    public class UseAdditiveBlending : PrimitiveCommand {}
}
