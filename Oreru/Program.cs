using Oreru.Domain;
using Oreru.Domain.Storyboards;
using Oreru.Osu;

var root = new EffectGroup
{
    Commands = [
        new PrimitiveCommand.Move
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
        },
    ],
    Children = [
        new StoryboardObject
        {
            ImagePath = "test.png",
            InitialPosition = new Vector2(250, 600),
            Commands = [
                new PrimitiveCommand.Flip
                {
                    Axis = Axis.Horizontal,
                    Interval = new Interval
                    {
                        StartTime = TimeSpan.FromMilliseconds(300),
                        EndTime = TimeSpan.FromMilliseconds(400)
                    }
                }
            ],
        },
        new StoryboardObject
        {
            ImagePath = "foo.jpg",
            InitialPosition = new Vector2(0, 0),
        }
    ]
};

var storyboard = new Storyboard { Effects = [root] };
var compiledStoryboard = new OsuStoryboardCompiler().Compile(storyboard);

using var file = File.CreateText("test.osb");
var serializer = new OsuStoryboardSerializer();
await serializer.Serialize(compiledStoryboard, file);