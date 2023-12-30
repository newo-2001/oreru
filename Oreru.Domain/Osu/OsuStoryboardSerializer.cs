using System.Text;

namespace Oreru.Domain.Osu;

/// <summary>
/// Serializes an <see cref="OsuStoryboard"/> into the <c>.osb</c> format recognized by osu!
/// </summary>
public class OsuStoryboardSerializer : ISerializer<OsuStoryboard>
{
    public async Task Serialize(OsuStoryboard storyboard, StreamWriter writer)
    {
        await writer.WriteLineAsync("[Events]");
        await writer.WriteLineAsync("//Background and video events");

        await writer.WriteLineAsync("//Storyboard Layer 0 (Background)");
        await WriteObjects(storyboard.GetLayer(OsuStoryboardLayer.Background), writer);
        
        await writer.WriteLineAsync("//Storyboard Layer 1 (Fail)");
        await WriteObjects(storyboard.GetLayer(OsuStoryboardLayer.Fail), writer);
        
        await writer.WriteLineAsync("//Storyboard Layer 2 (Pass)");
        await WriteObjects(storyboard.GetLayer(OsuStoryboardLayer.Pass), writer);

        await writer.WriteLineAsync("//Storyboard Layer 3 (Foreground)");
        await WriteObjects(storyboard.GetLayer(OsuStoryboardLayer.Foreground), writer);

        await writer.WriteLineAsync("//Storyboard Sound Samples");
    }

    private async Task WriteObjects(IEnumerable<OsuStoryboardObject> objects, StreamWriter writer)
    {
        foreach (var obj in objects)
        {
            await WriteObject(obj, writer);
        }
    }

    private async Task WriteObject(OsuStoryboardObject obj, StreamWriter writer)
    {
        var layer = obj.Layer switch
        {
            OsuStoryboardLayer.Background => "Background",
            OsuStoryboardLayer.Fail => "Fail",
            OsuStoryboardLayer.Pass => "Pass",
            OsuStoryboardLayer.Foreground => "Foreground",
            _ => throw new ArgumentException($"Unknown storyboard layer: {obj.Layer}")
        };

        var origin = obj.Origin switch
        {
            Origin.TopLeft => "TopLeft",
            Origin.Center => "Centre",
            Origin.CenterLeft => "CentreLeft",
            Origin.TopRight => "TopRight",
            Origin.BottomCenter => "BottomCentre",
            Origin.TopCenter => "TopCentre",
            Origin.Custom => "Custom",
            Origin.CenterRight => "CentreRight",
            Origin.BottomLeft => "BottomLeft",
            Origin.BottomRight => "BottomRight",
            _ => throw new ArgumentException($"Unknown origin: {obj.Origin}")
        };

        var serialized = new StringBuilder()
            .AppendJoin(',', [
                "Sprite",
                layer,
                origin,
                $"\"{obj.FilePath}\"",
                obj.InitialPosition.X,
                obj.InitialPosition.Y
            ]).ToString();

        await writer.WriteLineAsync(serialized);

        foreach (var command in obj.Commands.Select(SerializeCommand))
        {
            await writer.WriteLineAsync(command);
        }
    }

    private string SerializeCommand(OsuStoryboardCommand command)
    {
        var easing = (int) command.EasingFunction;
        var (tag, args) = command switch
        {
            OsuStoryboardCommand.Fade fade => ("F", new object[] { fade.Opacity.StartValue, fade.Opacity.EndValue }),
            OsuStoryboardCommand.Move move => ("M", [
                move.Position.StartValue.X,
                move.Position.StartValue.Y,
                move.Position.EndValue.X,
                move.Position.EndValue.Y
            ]),
            OsuStoryboardCommand.MoveX move => ("MX", [move.Position.StartValue, move.Position.EndValue]),
            OsuStoryboardCommand.MoveY move => ("MY", [move.Position.StartValue, move.Position.EndValue]),
            OsuStoryboardCommand.Scale scale => ("S", [scale.Scalar.StartValue, scale.Scalar.EndValue]),
            OsuStoryboardCommand.VectorScale scale => ("V", [
                scale.Vector.StartValue.X,
                scale.Vector.StartValue.Y,
                scale.Vector.EndValue.X,
                scale.Vector.EndValue.Y
            ]),
            OsuStoryboardCommand.Rotate rotation => ("R", [rotation.Angle.StartValue, rotation.Angle.EndValue]),
            OsuStoryboardCommand.FlipHorizontally => ("P", ['H']),
            OsuStoryboardCommand.FlipVertically => ("P", ['V']),
            OsuStoryboardCommand.UseAdditiveColorBlending => ("P", ['A']),
            _ => throw new ArgumentException("Unknown command")
        };

        return new StringBuilder()
            .Append(' ')
            .AppendJoin(',', [
                tag,
                easing,
                command.Interval.StartTime.TotalMilliseconds,
                command.Interval.EndTime.TotalMilliseconds,
                ..args
            ]).ToString();
    }
}
