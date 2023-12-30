using Oreru.Domain;
using Oreru.Domain.Osu;

OsuStoryboardCommand move = new OsuStoryboardCommand.Move
{
    Interval = new Interval
    {
        StartTime = TimeSpan.FromMilliseconds(5000),
        EndTime = TimeSpan.FromMilliseconds(7000),
    },
    Position = new Interpolated<Vector2>
    {
        StartValue = new Vector2(-3, 7),
        EndValue = new Vector2(-200, 700)
    }
};

var objects = new OsuStoryboardObject[]
{
    new OsuStoryboardObject
    {
        FilePath = "test.png",
        InitialPosition = new Vector2(50000, 200.3),
        Layer = OsuStoryboardLayer.Background,
        Origin = Origin.TopLeft,
        Commands = [move]
    }
};

var storyboard = new OsuStoryboard();
storyboard.AddObjects(objects);

using var file = File.CreateText("test.osb");

var serializer = new OsuStoryboardSerializer();
await serializer.Serialize(storyboard, file);
