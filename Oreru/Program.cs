using Oreru.Domain;
using Oreru.Domain.Storyboards;
using Oreru.Osu;

var root = new Effect
{
    Objects = [
        new StoryboardObject
        {
            ImagePath = "foo.jpg",
            InitialPosition = (0, 0),
        }
    ],
    Commands = [
        new StoryboardCommand.Move(new Interval(TimeSpan.FromMilliseconds(5000), TimeSpan.FromMilliseconds(7000)), (-3, 7))
    ],
    Children = [
        new Effect
        {
            Commands = [
                new StoryboardCommand.Flip(new Interval(TimeSpan.FromMilliseconds(300), TimeSpan.FromMilliseconds(400)), Axis.Horizontal)
            ],
            Objects = [
                new StoryboardObject
                {
                    ImagePath = "test.png",
                    InitialPosition = (250, 600),
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