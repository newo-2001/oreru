namespace Oreru.Domain.Osu;

/// <summary>
/// An individual command or transformation applied to an <see cref="Oreru.Domain.Osu.OsuStoryboardObject"/>.
/// </summary>
public abstract class OsuStoryboardCommand
{
    public required Interval Interval { get; set; }
    public EasingFunction EasingFunction { get; set; } = EasingFunction.Linear;

    public class Fade : OsuStoryboardCommand
    {
        public required Interpolated<double> Opacity { get; set; }
    }

    public class Move : OsuStoryboardCommand
    {
        public required Interpolated<Vector2> Position { get; set; }
    }

    public class MoveX : OsuStoryboardCommand
    {
        public required Interpolated<double> Position { get; set; }
    }

    public class MoveY : OsuStoryboardCommand
    {
        public required Interpolated<double> Position { get; set; }
    }

    public class Scale : OsuStoryboardCommand
    {
        public required Interpolated<double> Scalar { get; set; }
    }

    public class VectorScale : OsuStoryboardCommand
    {
        public required Interpolated<Vector2> Vector { get; set; }
    }

    public class Rotate : OsuStoryboardCommand
    {
        public required Interpolated<double> Angle { get; set; }
    }

    public class ChangeColor : OsuStoryboardCommand
    {
        public required Interpolated<Color> Color { get; set; }
    }

    public class FlipHorizontally : OsuStoryboardCommand;
    public class FlipVertically : OsuStoryboardCommand;
    public class UseAdditiveColorBlending : OsuStoryboardCommand;
}
