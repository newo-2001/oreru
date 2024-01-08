using AutoFixture;
using FluentAssertions;
using Oreru.Domain;
using Oreru.Domain.Storyboards;
using Xunit;

namespace Oreru.Osu.Tests;
public class OsuStoryboardCompilerTests
{
    private readonly OsuStoryboardCompiler _compiler = new OsuStoryboardCompiler();
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public void ChildEffectInheritsParentCommands()
    {
        var fade = _fixture.Create<StoryboardCommand.Fade>();
        var rotation = _fixture.Create<StoryboardCommand.Rotate>();

        var storyboard = new Storyboard
        {
            Effects = [
                new Effect
                {
                    Commands = [rotation],
                    Children = [
                        new Effect
                        {
                            Commands = [fade],
                            Objects = [_fixture.Create<StoryboardObject>()]
                        }
                    ]
                }
            ]
        };

        IEnumerable<OsuStoryboardCommand> actual = _compiler.Compile(storyboard).Objects.Single().Commands;
        actual.Should().BeEquivalentTo(new StoryboardCommand[] { fade, rotation });
    }

    [Fact]
    public void ParentEffectDoesNotInheritChildCommands()
    {
        var storyboard = new Storyboard()
        {
            Effects = [
                new Effect
                {
                    Objects = [_fixture.Create<StoryboardObject>()],
                    Children = [
                        new Effect
                        {
                            Commands = [_fixture.Create<StoryboardCommand.UseAdditiveBlending>()],
                        }
                    ]
                }
            ]
        };

        IEnumerable<OsuStoryboardCommand> actual = _compiler.Compile(storyboard).Objects.Single().Commands;
        actual.Should().BeEmpty();
    }

    [Fact]
    public void EffectOfSimultaneousMovesIsAdditive()
    {
        var storyboard = new Storyboard
        {
            Effects = [
                new Effect
                {
                    Commands = [
                        new StoryboardCommand.Move(new Interval(TimeSpan.Zero, TimeSpan.FromSeconds(10)), (10, 10))
                    ],
                    Children = [
                        new Effect
                        {
                            Objects = [
                                _fixture.Build<StoryboardObject>()
                                    .With(x => x.InitialPosition, (0, 0))
                                    .Create()
                            ],
                            Commands = [
                                new StoryboardCommand.Move(new Interval(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(15)), (10, 10))
                            ]
                        }
                    ]
                }
            ]
        };

        IEnumerable<OsuStoryboardCommand> actual = _compiler.Compile(storyboard).Objects.Single().Commands;
        actual.Should().BeEquivalentTo(new OsuStoryboardCommand[]
        {
            new OsuStoryboardCommand.Move(
                new Interval(TimeSpan.Zero, TimeSpan.FromSeconds(5)),
                new Interpolated<Vector2>((0, 0), (5, 5))
            ),
            new OsuStoryboardCommand.Move(
                new Interval(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10)),
                new Interpolated<Vector2>((5, 5), (15, 15))
            ),
            new OsuStoryboardCommand.Move(
                new Interval(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15)),
                new Interpolated<Vector2>((15, 15), (20, 20))
            )
        });
    }
}
