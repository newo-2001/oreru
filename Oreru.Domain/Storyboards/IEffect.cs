namespace Oreru.Domain.Storyboards;

/// <summary>
/// The central building block in oreru! storyboards.
/// Effects form a hierarchy, not unlike a scenegraph,
/// and contain objects and their associated transformations.
/// </summary>
public interface IEffect
{
    public IEnumerable<ICommand> Commands { get; }
}
