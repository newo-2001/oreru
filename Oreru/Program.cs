using Oreru.Domain;
using Oreru.Domain.Storyboards;
using Oreru.Osu;

var root = new Effect
{
    Objects = [
        new StoryboardObject
        {
            ImagePath = "foo.jpg",
            InitialPosition = new Vector2(0, 0),
        }
    ],
    Commands = [
        new StoryboardCommand.Move(new(new Vector2(-3, 7), new Vector2(-200, 700)))
        {
            Interval = new Interval
            {
                StartTime = TimeSpan.FromMilliseconds(5000),
                EndTime = TimeSpan.FromMilliseconds(7000),
            }
        }
    ],
    Children = [
        new Effect
        {
            Commands = [
                new StoryboardCommand.Flip(Axis.Horizontal)
                {
                    Interval = new Interval
                    {
                        StartTime = TimeSpan.FromMilliseconds(300),
                        EndTime = TimeSpan.FromMilliseconds(400)
                    }
                }
            ],
            Objects = [
                new StoryboardObject
                {
                    ImagePath = "test.png",
                    InitialPosition = new Vector2(250, 600),
                },
            ]
        },
    ]
};

var storyboard = new Storyboard { Effects = [root] };
var compiledStoryboard = new OsuStoryboardCompiler().Compile(storyboard);

using var file = File.CreateText("test.osb");
var serializer = new OsuStoryboardSerializer();
await serializer.Serialize(compiledStoryboard, file);