namespace Oreru.Domain.Storyboards;

/// <summary>
/// A composite command is an <see cref="ICommand"/>
/// composed of multiple other <see cref="ICommand"/>s.
/// </summary>
public interface ICompositeCommand : ICommand
{
    public IEnumerable<ICommand> Children { get; }

    Interval ICommand.Interval => new Interval
    {
        StartTime = Children.Select(x => x.Interval.StartTime).Min(),
        EndTime = Children.Select(x => x.Interval.EndTime).Max()
    };
}
